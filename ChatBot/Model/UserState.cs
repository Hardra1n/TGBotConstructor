using ChatBot.Handling.References;

namespace ChatBot.Model;

public class UserState
{
    public int? ScenarioId { get; set; } = null;

    public int? StepId { get; set; } = null;

    public UserState(int? scenarioId = null, int? stepId = null) 
    {
        ScenarioId = scenarioId;
        StepId = stepId;
    }

    public void ChangeState(int scenarioId, int stepId)
    {
        ScenarioId = scenarioId;
        StepId = stepId;
    }
}