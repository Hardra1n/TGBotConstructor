using ChatBot.Handling.Actions;
using ChatBot.Handling.References;

namespace ChatBot.Model;

public class BotCommand
{

    private BotAction _botAction;

    public BotCommand(string name, string description, BotAction botAction)
    {
        Name = name;
        Description = description;
        _botAction = botAction;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public async Task Call(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await _botAction.Execute(chatBot, receiverInfo);

    }
}