using Telegram.Bot;

namespace ChatBot.Model;
public class TelegramBot : IChatBot
{
    private TelegramBotClient _client;

    public TelegramBot(TelegramBotConfiguration configuration)
    {
        _client = new TelegramBotClient(configuration.Token);
        var botInfo = _client.GetMeAsync().Result;
        if (botInfo.FirstName != configuration.Name)
            _client.SetMyNameAsync(configuration.Name).Wait();
        if (configuration.Description != _client.GetMyDescriptionAsync().Result.Description)
            _client.SetMyDescriptionAsync(configuration.Description).Wait();
    }
}