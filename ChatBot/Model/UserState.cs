using ChatBot.Handling.References;

namespace ChatBot.Model;

public class UserState
{
    public int? ScenarioId = null;

    public int? StepId = null;

    public UserState() { }

    public void ChangeState(int scenarioId, int stepId)
    {
        ScenarioId = scenarioId;
        StepId = stepId;
    }
}