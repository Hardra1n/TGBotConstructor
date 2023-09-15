using Telegram.Bot.Types;

namespace ChatBot.Model;

public interface IChatBot
{
    void Start(CancellationTokenSource cts);

    Task SetCommands(IBotCommand[] commands);

    Task SendTextMessage(ReceiverInfo receiverInfo, string text, bool isReply = false);
}