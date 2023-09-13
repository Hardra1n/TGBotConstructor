namespace ChatBot.Model.Telegram;

public class TelegramBotConfiguration
{
    public TelegramBotConfiguration(string token, string name, string description)
    {
        Token = token;
        Name = name;
        Description = description;
    }

    public string Token { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

}