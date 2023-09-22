using System.Runtime.InteropServices;
using ChatBot.Handling.Actions;
using ChatBot.Handling.References;
using ChatBot.Model;

namespace ChatBot.Handling;

public class ScenarioStep
{
    public int Id { get; private set; }

    public BotAction Action { get; private set; }

    public IEnumerable<Reference>? _references = null;

    public ScenarioStep(int id, BotAction action)
    {
        Id = id;
        Action = action;
    }

    public void InitializeReferences(IEnumerable<Reference> references)
    {
        _references = references;
    }

    public override bool Equals(object? obj)
    {
        return obj == null ? false : Equals(obj as ScenarioStep);
    }

    public bool Equals(ScenarioStep? scenarioStep)
    {
        return scenarioStep != null && Id == scenarioStep.Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public async Task Call(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await Action.Execute(chatBot, receiverInfo);
    }
}