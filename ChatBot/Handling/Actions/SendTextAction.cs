using ChatBot.Handling.Actions;
using ChatBot.Model;

public class SendTextBotAction : BotAction
{
    private readonly string _text;

    public SendTextBotAction(string text)
    {
        _text = text;
    }

    public async override Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendTextMessage(receiverInfo, _text);
    }
}