using Telegram.Bot.Types;

namespace ChatBot.Model.Telegram;

public class TelegramBotCommand : IBotCommand
{
    public TelegramBotCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }

    public string Description { get; set; }
}