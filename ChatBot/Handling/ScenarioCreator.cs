using System.Text.Json.Nodes;
using ChatBot.Handling.Actions;

namespace ChatBot.Handling;

public static class ScenarioCreator
{
    public static HashSet<Scenario> GetScenariousHashSet()
    {
        HashSet<Scenario> scenarios = new();
        JsonArray scenariousNode = Extensions.GetScenariousJsonNodes();
        foreach (var scenarioNode in scenariousNode)
        {
            Scenario? scenario = CreateScenario(scenarioNode);
            if (scenario == null)
                continue;
            if (!scenarios.Add(scenario))
                System.Console.WriteLine($"Scenario with id {scenario.Id} already exist. Scenario must contain unique Id");
        }
        return scenarios;
    }

    private static Scenario? CreateScenario(JsonNode? scenarioNode)
    {
        if (scenarioNode == null)
            return null;
        int? scenarioId = (int?)scenarioNode["Id"]?.AsValue();
        if (scenarioId == null)
        {
            System.Console.WriteLine("Scenario must contain 'Id' property");
            return null;
        }
        JsonArray? stepsNode = scenarioNode["Steps"]?.AsArray();
        if (stepsNode == null)
        {
            System.Console.WriteLine("Scenario must contain 'Steps property'");
            return null;
        }

        Scenario scenario = new Scenario((int)scenarioId);
        foreach (var stepNode in stepsNode)
        {
            ScenarioStep? scenarioStep = CreateScenarioStep(stepNode);
            if (scenarioStep == null)
            {
                System.Console.WriteLine("Unable to create sceanario step");
                return null;
            }
            if (!scenario.Steps.Add(scenarioStep))
                System.Console.WriteLine($"Step with Id = {scenarioStep.Id} already exist. Id must be unique");
        }
        return scenario;
    }

    private static ScenarioStep? CreateScenarioStep(JsonNode? stepNode)
    {
        if (stepNode == null)
            return null;
        int? stepId = (int?)stepNode["Id"]?.AsValue();
        if (stepId == null)
        {
            System.Console.WriteLine("Step property must contain 'Id' property");
            return null;
        }
        JsonNode? actionNode = stepNode["Action"];
        if (actionNode == null)
        {
            System.Console.WriteLine("Step property must contain 'Action' property");
            return null;
        }
        BotAction? botAction = CommandCreator.CreateBotAction(actionNode);
        if (botAction == null)
        {
            System.Console.WriteLine("Unable to create scenario step. Bot action is wrong.");
            return null;
        }
        ScenarioStep scenarioStep = new((int)stepId, botAction);
        return scenarioStep;
    }
}