using ChatBot.Handling;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace ChatBot.Model.Telegram;
public class TelegramBot : IChatBot
{
    private TelegramBotClient _client;

    private ResponseHandler _handler;


    public TelegramBot(TelegramBotConfiguration configuration)
    {
        _client = new TelegramBotClient(configuration.Token);
        var botInfo = _client.GetMeAsync().Result;
        if (botInfo.FirstName != configuration.Name)
            _client.SetMyNameAsync(configuration.Name).Wait();
        if (configuration.Description != _client.GetMyDescriptionAsync().Result.Description)
            _client.SetMyDescriptionAsync(configuration.Description).Wait();
        _handler = new ResponseHandler(this);
    }


    public async Task SendTextMessage(ReceiverInfo reciever, string text, bool isReply = false)
    {
        int? messageToReply = null;
        if (isReply)
        {
            messageToReply = int.Parse(reciever.MessageId);
        }
        await _client.SendTextMessageAsync(reciever.Id, text, replyToMessageId: messageToReply);
    }

    public async Task SetCommands(IBotCommand[] commands)
    {
        try
        {
            var nativeCommands = commands.Select(cmd => (TelegramBotCommand)cmd);
            await _client.SetMyCommandsAsync(nativeCommands.Select(cmd => cmd.ToNativeBotCommand()));
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message + "\n Unable to convert IBotCommand to telegram commands");
            throw;
        }
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
            await _handler.HandleTextMessage(update.Message.ToReceiverInfo(), update.Message.Text);
        }

        return;
    }
}