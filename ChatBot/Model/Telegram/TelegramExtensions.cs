using ChatBot.Model;
using Telegram.Bot.Types;

namespace ChatBot.Model.Telegram;

public static class TelegramExtensions
{
    public static ReceiverInfo ToReceiverInfo(this Message message)
        => new ReceiverInfo(message.From!.FirstName, message.MessageId.ToString()) { Id = message.Chat.Id };

    public static BotCommand ToNativeBotCommand(this TelegramBotCommand command)
        => new BotCommand() { Command = command.Name, Description = command.Description };

    public static IEnumerable<IAlbumInputMedia> ConvertToAlbumInputMedia(
        this IEnumerable<KeyValuePair<string, string>> pairs)
    {
        List<IAlbumInputMedia> albumList = new();
        foreach (var mediaItem in pairs)
        {
            switch (mediaItem.Key)
            {
                case "Photo":
                    albumList.Add(new InputMediaPhoto(InputFile.FromFileId(mediaItem.Value)));
                    break;
                case "Document":
                    albumList.Add(new InputMediaDocument(InputFile.FromFileId(mediaItem.Value)));
                    break;
                case "Video":
                    albumList.Add(new InputMediaVideo(InputFile.FromFileId(mediaItem.Value)));
                    break;
                case "Audio":
                    albumList.Add(new InputMediaAudio(InputFile.FromFileId(mediaItem.Value)));
                    break;
                default:
                    throw new Exception("Unrecognized File Type for album");
            }
        }
        return albumList;
    }
}