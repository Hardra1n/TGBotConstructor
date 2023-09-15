using ChatBot.Model;

namespace ChatBot.Handling.Actions;
public abstract class BotAction
{
    public abstract Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo);
}