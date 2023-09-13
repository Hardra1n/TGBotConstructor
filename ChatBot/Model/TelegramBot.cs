using ChatBot.Handling;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

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

    private ResponseHandler Handler => new ResponseHandler(this);

    public async Task SendTextMessage(ReceiverInfo reciever, string text)
    {
        await _client.SendTextMessageAsync(reciever.Id, text);
    }

    public void Start(CancellationTokenSource cts)
    {
        _client.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: new ReceiverOptions(),
            cancellationToken: cts.Token);
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if (update.Message != null && update.Message.Text != null)
        {
            await Handler.HandleTextMessage(update.Message.ToReceiverInfo(), update.Message.Text);
        }

        return;
    }
}