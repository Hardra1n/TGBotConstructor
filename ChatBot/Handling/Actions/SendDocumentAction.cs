using ChatBot.Model;

namespace ChatBot.Handling.Actions;

public class SendDocumentAction : BotAction
{
    private readonly string _fileId;
    private readonly string? _caption;

    public SendDocumentAction(string fileId, string? caption)
    {
        _fileId = fileId;
        _caption = caption == string.Empty ? null : caption;
    }

    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendDocumentMessage(receiverInfo, _fileId, _caption);
    }
}
