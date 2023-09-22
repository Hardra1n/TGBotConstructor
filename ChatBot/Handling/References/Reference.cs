
namespace ChatBot.Handling.References;

public class Reference
{
    public Reference(int scenarioId, int stepId, Call call)
    {
        ScenarioId = scenarioId;
        StepId = stepId;
        Call = call;
    }

    public int ScenarioId { get; private set; }

    public int StepId { get; private set; }

    public Call Call { get; private set; }

    internal bool IsMyCallKey(object key)
        => Call.IsMyKey(key);
}