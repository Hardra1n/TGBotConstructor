using Telegram.Bot.Types;

namespace ChatBot.Model;

public interface IChatBot
{
    void Start(CancellationTokenSource cts);

    Task SetCommands(IEnumerable<BotCommand> commands);

    Task SendTextMessage(ReceiverInfo receiverInfo, string text, bool isReply);

    Task SendPhotoMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply);

    Task SendAudioMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply);

    Task SendVoiceMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply);

    Task SendVideoMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply);

    Task SendVideoNoteMessage(ReceiverInfo receiverInfo, string fileId, bool isReply);

    Task SendDocumentMessage(ReceiverInfo receiverInfo, string fileId, string? caption, bool isReply);

    Task SendAlbumMessage(ReceiverInfo receiverInfo, IEnumerable<KeyValuePair<string, string>> typeFileIdPairs, bool isReply);
}