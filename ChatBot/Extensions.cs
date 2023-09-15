using System.Text.Json;
using System.Text.Json.Nodes;
using ChatBot.Model.Telegram;
using Telegram.Bot.Types;

public static class Extensions
{
    public static TelegramBotConfiguration GetTelegramBotConfigurationFromJson()
    {
        try
        {
            string jsonConfig = System.IO.File.ReadAllText("./BotConfiguration.json");
            var botConfig = JsonSerializer.Deserialize<TelegramBotConfiguration>(jsonConfig);
            if (botConfig != null)
                return botConfig;
            else
                throw new Exception("botConfig == null");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Unable to convert json to telegram configuration object.\n {ex.Message}");
            throw;
        }
    }

    public static IBotCommand[] GetTelegramBotCommandsFromJson()
    {
        try
        {
            string jsonCommands = System.IO.File.ReadAllText("./BotMessages.json");
            JsonNode jsonNode = JsonNode.Parse(jsonCommands)!;
            IBotCommand[] botCommands = jsonNode["Commands"].Deserialize<TelegramBotCommand[]>()
                ?? Array.Empty<IBotCommand>();
            return botCommands;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Unable to convert json to telegram command objects. \n {ex.Message}");
            throw;
        }
    }

    public static IEnumerable<KeyValuePair<string, JsonNode>> GetCommandActionJsonNodePairs()
    {
        try
        {
            string jsonCommands = System.IO.File.ReadAllText("./BotMessages.json");
            JsonNode jsonNode = JsonNode.Parse(jsonCommands)!;
            var commandsNode = jsonNode["Commands"]!.AsArray();
            List<KeyValuePair<string, JsonNode>> keyValuesList = new();
            foreach (var node in commandsNode)
            {
                string commandName = '/' + node!["Name"]!.AsValue().ToString();
                JsonNode actionNode = node!["Action"]!;
                if (actionNode == null)
                    continue;
                keyValuesList.Add(KeyValuePair.Create(commandName, actionNode));
            }
            return keyValuesList;

        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Unable to convert json command's action nodes. \n {ex.Message}");
            throw;
        }
    }
}