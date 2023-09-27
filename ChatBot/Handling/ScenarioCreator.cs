using System.Collections.Immutable;
using System.Text.Json.Nodes;
using ChatBot.Handling.Actions;

namespace ChatBot.Handling;

public static class ScenarioCreator
{
    public static HashSet<Scenario> GetScenariousHashSet()
    {
        HashSet<Scenario> scenarious = new();
        JsonArray scenariousNode = Extensions.GetScenariousJsonNodes();
        foreach (var scenarioNode in scenariousNode)
        {
            Scenario? scenario = CreateScenario(scenarioNode);
            if (scenario == null)
                continue;
            if (!scenarious.Add(scenario))
                System.Console.WriteLine($"Scenario with id {scenario.Id} already exist. Scenario must contain unique Id");
        }
        return scenarious;
    }

    public static HashSet<Scenario>? InitStepReferences(HashSet<Scenario> scenarious)
    {
        JsonArray scenariousNode = Extensions.GetScenariousJsonNodes();
        foreach (var scenarioNode in scenariousNode)
        {
            if (scenarioNode == null)
                continue;
            int? scenarioId = (int?)scenarioNode["Id"]?.AsValue();
            if (scenarioId == null)
            {
                continue;
            }
            JsonArray? stepsNode = scenarioNode["Steps"]?.AsArray();
            if (stepsNode == null)
            {
                continue;
            }

            foreach (var stepNode in stepsNode)
            {
                if (stepNode == null)
                    continue;
                int? stepId = (int?)stepNode["Id"]?.AsValue();
                if (stepId == null)
                {
                    continue;
                }

                var scenario = scenarious.FirstOrDefault(scr => scr.Id == scenarioId);
                var step = scenario?.Steps.FirstOrDefault(step => step.Id == stepId);
                step?.InitializeReferences(ReferenceCreator.CreateReferences(stepNode));
            }

        }
        return scenarious;
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
        JsonArray? actionNode = stepNode["Actions"]?.AsArray();
        if (actionNode == null)
        {
            System.Console.WriteLine("Step property must contain 'Action' property");
            return null;
        }
        IEnumerable<BotAction>? botAction = CommandCreator.CreateBotActions(actionNode);
        if (botAction == null)
        {
            System.Console.WriteLine("Unable to create scenario step. Bot action is wrong.");
            return null;
        }
        ScenarioStep scenarioStep = new((int)stepId, botAction);
        return scenarioStep;
    }
}