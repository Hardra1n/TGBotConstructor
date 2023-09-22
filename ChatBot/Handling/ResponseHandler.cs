using ChatBot.Handling.Actions;
using ChatBot.Model;
using Telegram.Bot.Polling;

namespace ChatBot.Handling;

public class ResponseHandler
{
    private IChatBot _chatBot;

    private IEnumerable<BotCommand> _commands;

    private TelegramAdminRepository _adminRepository;

    public ResponseHandler(IChatBot chatBot)
    {
        _chatBot = chatBot;
        _commands = CommandCreator.GetBotCommands();
        _adminRepository = Extensions.GetTelegramAdminRepository();
    }

    public async Task HandleTextMessage(ReceiverInfo receiverInfo, string text)
    {
        var commandToExecute = _commands.FirstOrDefault(cmd => '/' + cmd.Name == text);
        if (commandToExecute != null)
        {
            await commandToExecute.Call(_chatBot, receiverInfo);
            return;
        }

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