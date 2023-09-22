using ChatBot.Handling.Actions;
using ChatBot.Handling.References;

namespace ChatBot.Model;

public class BotCommand
{
    private BotAction _botAction;

    private IEnumerable<Reference> _references;

    public BotCommand(string name, string description, BotAction botAction, IEnumerable<Reference> references)
    {
        Name = name;
        Description = description;
        _botAction = botAction;
        _references = references;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public async Task Call(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await _botAction.Execute(chatBot, receiverInfo);

    }

    public Reference? GetReferenceByKey(object key)
    {
        return _references.FirstOrDefault(reference => reference.IsMyCallKey(key));
    }
}