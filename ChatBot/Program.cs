using System.Text.Json;
using System.Text.Json.Nodes;
using ChatBot.Model;
using ChatBot.Model.Telegram;

namespace ChatBot;
internal class Program
{
    private static void Main(string[] args)
    {
        var botConfig = Extensions.GetTelegramBotConfigurationFromJson();

        var botCommands = Extensions.GetTelegramBotCommandsFromJson();


        IChatBot chatBot = new TelegramBot(botConfig);
        using CancellationTokenSource cts = new();
        chatBot.SetCommands(botCommands);
        chatBot.Start(cts);

        Console.WriteLine("Bot was started");
        Console.ReadLine();
        cts.Cancel();
    }
}