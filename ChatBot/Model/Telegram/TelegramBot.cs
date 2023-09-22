using System.Security.Authentication.ExtendedProtection;
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

    public async Task SendAlbumMessage(ReceiverInfo receiverInfo, IEnumerable<KeyValuePair<string, string>> typeFileIdPairs, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendMediaGroupAsync(receiverInfo.Id, typeFileIdPairs.ConvertToAlbumInputMedia(), replyToMessageId: messageToReply);
    }

    public async Task SendAudioMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendAudioAsync(receiverInfo.Id, InputFile.FromFileId(fileId), caption: caption, replyToMessageId: messageToReply);
    }

    public async Task SendDocumentMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendDocumentAsync(receiverInfo.Id, InputFile.FromFileId(fileId), caption: caption, replyToMessageId: messageToReply);
    }

    public async Task SendPhotoMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendPhotoAsync(receiverInfo.Id, InputFile.FromFileId(fileId), caption: caption, replyToMessageId: messageToReply);
    }

    public async Task SendTextMessage(ReceiverInfo receiverInfo, string text, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendTextMessageAsync(receiverInfo.Id, text, replyToMessageId: messageToReply);
    }

    public async Task SendVideoMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendVideoAsync(receiverInfo.Id, InputFile.FromFileId(fileId), caption: caption, replyToMessageId: messageToReply);
    }

    public async Task SendVideoNoteMessage(ReceiverInfo receiverInfo, string fileId, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendVideoNoteAsync(receiverInfo.Id, InputFile.FromFileId(fileId), replyToMessageId: messageToReply);
    }

    public async Task SendVoiceMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply)
    {
        int? messageToReply = isReply ? int.Parse(receiverInfo.MessageId) : null;
        await _client.SendVoiceAsync(receiverInfo.Id, InputFile.FromFileId(fileId), caption: caption, replyToMessageId: messageToReply);
    }

    public async Task SetCommands(IEnumerable<BotCommand> commands)
    {
        try
        {
            await _client.SetMyCommandsAsync(commands.Select(cmd => cmd.ToNativeBotCommand()));
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
        if (update.Message != null)
        {
            // Handling text message
            if (update.Message.Text != null)
            {
                await _handler.HandleTextMessage(update.Message.ToReceiverInfo(), update.Message.Text);
                return;
            }

            if (update.Message.Audio != null)
            {
                await _handler.HandleMediaMessage(update.Message.ToReceiverInfo(), update.Message.Audio.FileId);
                return;
            }

            if (update.Message.Video != null)
            {
                await _handler.HandleMediaMessage(update.Message.ToReceiverInfo(), update.Message.Video.FileId);
                return;
            }
            if (update.Message.VideoNote != null)
            {
                await _handler.HandleMediaMessage(update.Message.ToReceiverInfo(), update.Message.VideoNote.FileId);
                return;
            }

            if (update.Message.Photo != null)
            {
                await _handler.HandleMediaMessage(update.Message.ToReceiverInfo(), update.Message.Photo.First().FileId);
                return;
            }

            if (update.Message.Voice != null)
            {
                await _handler.HandleMediaMessage(update.Message.ToReceiverInfo(), update.Message.Voice.FileId);
                return;
            }

            if (update.Message.Document != null)
            {
                await _handler.HandleMediaMessage(update.Message.ToReceiverInfo(), update.Message.Document.FileId);
                return;
            }

        }
    }
}