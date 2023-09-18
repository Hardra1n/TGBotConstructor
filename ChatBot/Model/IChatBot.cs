using Telegram.Bot.Types;

namespace ChatBot.Model;

public interface IChatBot
{
    void Start(CancellationTokenSource cts);

    Task SetCommands(IBotCommand[] commands);

    Task SendTextMessage(ReceiverInfo receiverInfo, string text, bool isReply = false);

    Task SendPhotoMessage(ReceiverInfo receiverInfo, string fileId, string? caption);

    Task SendAudioMessage(ReceiverInfo receiverInfo, string fileId, string? caption);

    Task SendVoiceMessage(ReceiverInfo receiverInfo, string fileId, string? caption);

    Task SendVideoMessage(ReceiverInfo receiverInfo, string fileId, string? caption);

    Task SendVideoNoteMessage(ReceiverInfo receiverInfo, string fileId);

    Task SendDocumentMessage(ReceiverInfo receiverInfo, string fileId, string? caption);
}