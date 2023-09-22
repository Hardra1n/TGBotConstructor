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
            var jsonNode = GetJsonFileAsNode("./BotConfiguration.json");
            var botConfig = jsonNode.Deserialize<TelegramBotConfiguration>();
            if (botConfig == null)
                throw new Exception("botConfig == null");

            return botConfig;

        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Unable to convert json to telegram configuration object.\n {ex.Message}");
            throw;
        }
    }

    public static IEnumerable<KeyValuePair<string, JsonNode>> GetCommandActionJsonNodePairs()
    {
        try
        {
            var jsonNode = GetJsonFileAsNode("./BotMessages.json");
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

    public static TelegramAdminRepository GetTelegramAdminRepository()
    {
        try
        {
            var jsonNode = GetJsonFileAsNode("./Admins.json");
            var adminRepository = jsonNode.Deserialize<TelegramAdminRepository>();
            if (adminRepository == null)
                throw new NullReferenceException();
            return adminRepository;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Unable to get admin repository from json file. \n {ex.Message}");
            throw;
        }
    }

    public static async Task UpdateTelegramAdminRepository(TelegramAdminRepository adminRepository)
    {

        try
        {
            await using FileStream fileStream = System.IO.File.Create("./Admins.json");
            await JsonSerializer.SerializeAsync(fileStream, adminRepository);

        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Unable to add admin member to json. \n {ex.Message}");
            throw;
        }
    }

    public static JsonArray GetScenariousJsonNodes()
    {
        try
        {
            var node = GetJsonFileAsNode("./BotMessages.json");
            JsonArray? scenariousNodes = node["Scenarious"]?.AsArray();
            if (scenariousNodes == null)
                throw new Exception("Unable to find 'Scenarious' property in json");
            return scenariousNodes;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Unable to get scenarious from json. \n {ex.Message}");
            throw;
        }
    }

    public static JsonNode GetJsonFileAsNode(string jsonFile)
    {

        string jsonfile = System.IO.File.ReadAllText(jsonFile);
        JsonNode? node = JsonNode.Parse(jsonfile);
        if (node == null)
            throw new Exception($"Unable to parse file to json");
        return node;
    }

    public static JsonNode GetCommandsJsonNode()
    {
        var jsonNode = GetJsonFileAsNode("BotMessages.json");
        var commandsNode = jsonNode["Commands"];
        if (commandsNode == null)
            throw new Exception("Unable to get node 'Commands'");
        return commandsNode;
    }
}