using ChatBot.Handling;
using ChatBot.Handling.References;
using ChatBot.Model;

public class ScenarioRepository
{
    private HashSet<Scenario> _scenarious;

    public ScenarioRepository()
    {
        _scenarious = ScenarioCreator.GetScenariousHashSet();
        ScenarioCreator.InitStepReferences(_scenarious);
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

    public ScenarioStep? GetScenarioStep(int scenarioId, int stepId)
    {
        var scenario = _scenarious.FirstOrDefault(scr => scr.Id == scenarioId);
        var step = scenario?.Steps.FirstOrDefault(step => step.Id == stepId);
        return step;
    }

    public ScenarioStep? GetScenarioStep(UserState userState)
    {
        if (userState.ScenarioId != null && userState.StepId != null)
        {
            return GetScenarioStep((int)userState.ScenarioId, (int)userState.StepId);
        }
        return null;
    }

    public async Task CallResponseByReference(IChatBot chatBot, ReceiverInfo receiverInfo, Reference reference, UserState userState)
    {
        var step = GetScenarioStep(reference.ScenarioId, reference.StepId);
        if (step != null)
        {
            await step.Call(chatBot, receiverInfo);
            userState.ChangeState(reference.ScenarioId, step.Id);
        }
    }
    public async Task FindAndCallResponseByText(IChatBot chatBot, ReceiverInfo receiverInfo, string text, UserState userState)
    {
        var step = GetScenarioStep(userState);
        if (step != null)
        {
            var reference = step._references?.FirstOrDefault(reference => reference.IsMyCallKey(text));
            if (reference != null)
                await CallResponseByReference(chatBot, receiverInfo, reference, userState);
        }
    }
}
