using System.Text.Json;
using System.Text.Json.Nodes;
using ChatBot.Model.Telegram;

public static class Extensions
{
    public static TelegramBotConfiguration GetTelegramBotConfigurationFromJson()
    {
        try
        {
            string jsonConfig = File.ReadAllText("./BotConfiguration.json");
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
            string jsonCommands = File.ReadAllText("./BotMessages.json");
            if (jsonCommands == null)
                throw new NullReferenceException();
            JsonNode jsonNode = JsonNode.Parse(jsonCommands)!;
            if (jsonNode == null)
                throw new NullReferenceException();
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
}