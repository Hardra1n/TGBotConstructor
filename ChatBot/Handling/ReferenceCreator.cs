using System.Text.Json.Nodes;
using ChatBot.Handling;
using ChatBot.Handling.References;

public class ReferenceCreator
{

    public static IEnumerable<Reference> CreateReferences(JsonNode nodeWithReference)
    {
        List<Reference> references = new List<Reference>();
        ScenarioRepository scenarioRepository = new();
        var referenceArray = nodeWithReference["Reference"]?.AsArray();
        if (referenceArray != null && referenceArray.Count != 0)
            foreach (var referenceNode in referenceArray)
            {
                if (referenceNode == null)
                    continue;

                var call = CreateCall(referenceNode);
                if (call == null)
                    continue;

                var scenarioIdStr = referenceNode["Scenario"]?.AsValue().ToString();
                var stepIdStr = referenceNode["Step"]?.AsValue().ToString();
                if (scenarioIdStr != null && int.TryParse(scenarioIdStr, out int scenarioId)
                    && stepIdStr != null && int.TryParse(stepIdStr, out int stepId))
                {
                    if (scenarioRepository.Contains(scenarioId, stepId))
                        references.Add(new Reference(scenarioId, stepId, call));
                }
            }
        return references;
    }

    private static Call? CreateCall(JsonNode referenceNode)
    {
        try
        {
            var callMethodStr = referenceNode["CallMethod"]?.AsValue().ToString();
            if (callMethodStr == null)
            {
                System.Console.WriteLine("'Reference' property must contain 'CallMethod' property");
                return null;
            }

            var callMethod = Enum.Parse<CallMethod>(callMethodStr);
            Call? call;
            switch (callMethod)
            {
                case CallMethod.Text:
                    call = CreateTextCall(referenceNode);
                    break;
                default:
                    call = null;
                    break;
            }

            if (call == null)
                System.Console.WriteLine("Unable to create call");

            return call;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"'CallMethod' property value is incorrect \n {ex.Message}");
            return null;
        }

    }

    private static TextCall? CreateTextCall(JsonNode referenceNode)
    {
        var callValue = referenceNode["CallValue"]?.AsValue().ToString();
        if (callValue == null || callValue == string.Empty)
        {
            System.Console.WriteLine("Reference property must contain not empty 'CallValue' property");
            return null;
        }
        return new TextCall(callValue);
    }
}