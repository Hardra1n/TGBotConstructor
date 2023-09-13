using ChatBot.Model;
using Telegram.Bot.Types;

namespace ChatBot.Model.Telegram;

public static class TelegramExtensions
{
    public static ReceiverInfo ToReceiverInfo(this Message message)
        => new ReceiverInfo(message.From!.FirstName) { Id = message.Chat.Id };

    public static BotCommand ToNativeBotCommand(this TelegramBotCommand command)
        => new BotCommand() { Command = command.Name, Description = command.Description };
}