using ChatBot.Handling.Actions;
using ChatBot.Model;
using Telegram.Bot.Polling;

namespace ChatBot.Handling;

public class ResponseHandler
{
    private IChatBot _chatBot;

    private IDictionary<string, BotAction> _textHandlers;

    public ResponseHandler(IChatBot chatBot)
    {
        _chatBot = chatBot;
        _textHandlers = ActionCreator.GetTextBotActionsDictionary();
    }

    public async Task HandleTextMessage(ReceiverInfo receiverInfo, string text)
    {
        BotAction? action;
        if (_textHandlers.TryGetValue(text, out action))
            await action.Execute(_chatBot, receiverInfo);
    }
}