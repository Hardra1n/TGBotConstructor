using System.Text.Json;
using ChatBot.Model;

namespace ChatBot;
internal class Program
{
    private static void Main(string[] args)
    {
        string jsonConfig = File.ReadAllText("./BotConfiguration.json");
        var botConfig = JsonSerializer.Deserialize<TelegramBotConfiguration>(jsonConfig);
        if (botConfig == null)
            return;

        IChatBot chatBot = new TelegramBot(botConfig);
    }
}