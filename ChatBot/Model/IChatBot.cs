using Telegram.Bot.Types;

namespace ChatBot.Model;

public interface IChatBot
{
    void Start(CancellationTokenSource cts);

    Task SendTextMessage(ReceiverInfo receiverInfo, string text);
}