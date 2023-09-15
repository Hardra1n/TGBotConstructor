using ChatBot.Handling.Actions;
using ChatBot.Model;

public class SendTextBotAction : BotAction
{
    private readonly string _text;
    private readonly bool _isReply;

    public SendTextBotAction(string text, bool isReply = false)
    {
        _isReply = isReply;
        _text = text;
    }

    public async override Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendTextMessage(receiverInfo, _text, _isReply);
    }
}