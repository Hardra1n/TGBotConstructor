using ChatBot.Model;
using Telegram.Bot.Polling;

namespace ChatBot.Handling;

public class ResponseHandler
{
    private IChatBot _chatBot;

    public ResponseHandler(IChatBot chatBot)
    {
        _chatBot = chatBot;
    }

    public async Task HandleTextMessage(ReceiverInfo receiverInfo, string text)
    {
        string response = $"I saw your text \"{text}\", thank you," +
            $"{receiverInfo.Name ?? "anonymous"}, chatId {receiverInfo.Id}";

        await _chatBot.SendTextMessage(receiverInfo, response);
    }
}