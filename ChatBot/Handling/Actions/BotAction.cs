using ChatBot.Model;

namespace ChatBot.Handling.Actions;
public abstract class BotAction
{
    protected readonly bool _isReply;

    public BotAction(bool isReply)
    {
        _isReply = isReply;
    }

    public abstract Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo);
}