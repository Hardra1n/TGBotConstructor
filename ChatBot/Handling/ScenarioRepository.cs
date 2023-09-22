using ChatBot.Handling;
using ChatBot.Handling.References;
using ChatBot.Model;

public class ScenarioRepository
{
    private HashSet<Scenario> _scenarious;

    public ScenarioRepository()
    {
        _scenarious = ScenarioCreator.GetScenariousHashSet();
    }

    public bool Contains(int scenarioId, int stepId)
    {
        try
        {
            var scenario = _scenarious.First(scr => scr.Id == scenarioId);
            var step = scenario.Steps.First(step => step.Id == stepId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public ScenarioStep? GetScenarioStepById(int scenarioId, int stepId)
    {
        var scenario = _scenarious.FirstOrDefault(scr => scr.Id == scenarioId);
        var step = scenario?.Steps.FirstOrDefault(step => step.Id == stepId);
        return step;
    }

    public async Task CallResponseByReference(IChatBot chatBot, ReceiverInfo receiverInfo, Reference reference)
    {
        var step = GetScenarioStepById(reference.ScenarioId, reference.StepId);
        if (step != null)
            await step.Call(chatBot, receiverInfo);
    }
}
