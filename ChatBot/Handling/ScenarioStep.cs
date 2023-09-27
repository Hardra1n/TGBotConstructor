using System.Runtime.InteropServices;
using ChatBot.Handling.Actions;
using ChatBot.Handling.References;
using ChatBot.Model;

namespace ChatBot.Handling;

public class ScenarioStep
{
    public int Id { get; private set; }

    public IEnumerable<BotAction> Actions { get; private set; }

    public IEnumerable<Reference>? _references = null;

    public ScenarioStep(int id, IEnumerable<BotAction> actions)
    {
        Id = id;
        Actions = actions;
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
        foreach (var action in Actions)
        {
            await action.Execute(chatBot, receiverInfo);
        }
    }
}