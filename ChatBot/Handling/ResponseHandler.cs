using ChatBot.Handling.Actions;
using ChatBot.Model;
using Telegram.Bot.Polling;

namespace ChatBot.Handling;

public class ResponseHandler
{
    private IChatBot _chatBot;

    private IEnumerable<BotCommand> _commands;

    private ScenarioRepository _scenarioRepository;

    private UserStateRepository _userStateRepository;

    private TelegramAdminRepository _adminRepository;

    public ResponseHandler(IChatBot chatBot)
    {
        _chatBot = chatBot;
        _commands = CommandCreator.GetBotCommands();
        _adminRepository = Extensions.GetTelegramAdminRepository();
        _scenarioRepository = new();
        _userStateRepository = new();
    }

    public async Task HandleTextMessage(ReceiverInfo receiverInfo, string text)
    {
        UserState userState = _userStateRepository.GetOrCreate(receiverInfo.Id);
        var commandToExecute = _commands.FirstOrDefault(cmd => '/' + cmd.Name == text);
        if (commandToExecute != null)
        {
            await commandToExecute.Call(_chatBot, receiverInfo);
            return;
        }

        // Try to navigate to scenarious by response to command (command's reference)
        var reference = _commands.FirstOrDefault(cmd => cmd.GetReferenceByKey(text) != null)?.GetReferenceByKey(text);
        if (reference != null)
        {
            await _scenarioRepository.CallResponseByReference(_chatBot, receiverInfo, reference, userState);
            return;
        }

        await _scenarioRepository.FindAndCallResponseByText(_chatBot, receiverInfo, text, userState);

        // Creation admin
        if (text == _adminRepository.SecretPhrase)
        {
            await _adminRepository.AddAdminMember(receiverInfo.Id.ToString());
            await _chatBot.SendTextMessage(receiverInfo, "You were added to admin memebers", true);
            return;
        }
    }

    public async Task HandleMediaMessage(ReceiverInfo receiverInfo, string fileId)
    {
        if (_adminRepository.Members.Contains(receiverInfo.Id.ToString()))
        {
            await _chatBot.SendTextMessage(receiverInfo, $"This File Id: \"{fileId}\"", true);
        }
    }
}