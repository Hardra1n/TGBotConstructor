using ChatBot.Handling.Actions;
using ChatBot.Model;

public class SendTextBotAction : SendBotAction
{
    private readonly string _text;

    public SendTextBotAction(string text, bool isReply) : base(isReply)
    {
        _text = text;
    }

    public async override Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendTextMessage(receiverInfo, _text, _isReply);
    }
}