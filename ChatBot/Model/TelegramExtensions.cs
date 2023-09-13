using ChatBot.Model;
using Telegram.Bot.Types;

namespace ChatBot.Model;
public static class TelegramExtensions
{
    public static ReceiverInfo ToReceiverInfo(this Message message) => new ReceiverInfo(message.From!.FirstName)
    {
        Id = message.Chat.Id
    };
}