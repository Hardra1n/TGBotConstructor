using ChatBot.Handling.Actions;
using ChatBot.Model;

namespace ChatBot.Handling.Actions;

public class SendBotAction : BotAction
{
    protected bool _isReply;

    public SendBotAction(bool isReply)
    {
        _isReply = isReply;
    }

    public override Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        throw new NotImplementedException();
    }
}
