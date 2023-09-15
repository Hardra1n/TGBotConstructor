using ChatBot.Handling.Actions;
using ChatBot.Model;
using Telegram.Bot.Polling;

namespace ChatBot.Handling;

public class ResponseHandler
{
    private IChatBot _chatBot;

    private Dictionary<string, BotAction> _textHandlers;

    public ResponseHandler(IChatBot chatBot)
    {
        _chatBot = chatBot;
        _textHandlers = new Dictionary<string, BotAction>()
        {
            {"/help", new SendTextBotAction("You want to help you?") },
            {"/help2", new SendTextBotAction("Ah, really?")}
        };
    }

    public async Task HandleTextMessage(ReceiverInfo receiverInfo, string text)
    {
        var action = _textHandlers.GetValueOrDefault(text);
        if (action != null)
            await action.Execute(_chatBot, receiverInfo);
    }
}