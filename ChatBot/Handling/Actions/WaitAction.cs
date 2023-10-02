using System.Security.Cryptography;
using ChatBot.Handling.Actions;
using ChatBot.Model;

public class WaitAction : BotAction
{
    private TimeSpan _time;

    public WaitAction(TimeSpan time)
    {
        _time = time;
    }

    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await Task.Delay(_time);
    }
}