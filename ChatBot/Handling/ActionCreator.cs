using System.Text.Json.Nodes;
using ChatBot.Handling.Actions;

public class ActionCreator
{
    public static IDictionary<string, BotAction> GetTextBotActionsDictionary()
    {
        var textJsonNodePairs = Extensions.GetCommandActionJsonNodePairs();
        var textBotActionDictionary = new Dictionary<string, BotAction>();
        foreach (var pair in textJsonNodePairs)
        {
            BotAction? botAction = CreateBotAction(pair.Value);
            if (botAction != null)
                textBotActionDictionary.Add(pair.Key, botAction);

        }
        return textBotActionDictionary;
    }

    private static BotAction? CreateBotAction(JsonNode actionNode)
    {
        string actionName = actionNode["Name"]?.AsValue().ToString()!;
        if (actionName == null)
        {
            Console.WriteLine("Action property in json doesn't contain 'Name' property");
        }

        BotAction? botAction;
        switch (actionName)
        {
            case "Text":
                botAction = CreateSendTextBotAction(actionNode);
                break;
            default:
                botAction = null;
                break;
        }
        if (botAction == null)
        {
            Console.WriteLine("Action property incorrect");
            return null;
        }
        return botAction;
    }

    private static SendTextBotAction? CreateSendTextBotAction(JsonNode jsonNode)
    {
        string textToWrite = jsonNode["Value"]?.AsValue().ToString()!;
        if (textToWrite == null)
        {
            Console.WriteLine("Text Action property in json doesn't contain 'Value' property");
            return null;
        }
        return new SendTextBotAction(textToWrite);
    }
}