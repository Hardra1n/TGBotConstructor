using ChatBot.Handling.Actions;
using ChatBot.Handling.References;

namespace ChatBot.Model;

public class BotCommand
{
    private IEnumerable<BotAction> _botActions;

    private IEnumerable<Reference> _references;

    public BotCommand(string name, string description, IEnumerable<BotAction> botActions, IEnumerable<Reference> references)
    {
        Name = name;
        Description = description;
        _botActions = botActions;
        _references = references;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public async Task Call(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        foreach (var action in _botActions)
        {
            await action.Execute(chatBot, receiverInfo);
        }

    }

    public Reference? GetReferenceByKey(object key)
    {
        return _references.FirstOrDefault(reference => reference.IsMyCallKey(key));
    }
}