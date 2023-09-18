using ChatBot.Handling.Actions;
using ChatBot.Model;
using Telegram.Bot.Polling;

namespace ChatBot.Handling;

public class ResponseHandler
{
    private IChatBot _chatBot;

    private IDictionary<string, BotAction> _textHandlers;

    private TelegramAdminRepository _adminRepository;

    public ResponseHandler(IChatBot chatBot)
    {
        _chatBot = chatBot;
        _textHandlers = ActionCreator.GetTextBotActionsDictionary();
        _adminRepository = Extensions.GetTelegramAdminRepository();
    }

    public async Task HandleTextMessage(ReceiverInfo receiverInfo, string text)
    {
        BotAction? action;
        if (_textHandlers.TryGetValue(text, out action))
        {
            await action.Execute(_chatBot, receiverInfo);
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
}