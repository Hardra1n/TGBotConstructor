using ChatBot.Handling.Actions;
using ChatBot.Model;

public class SendPhotoAction : BotAction
{
    private readonly string _fileId;
    private readonly string? _caption;
    public SendPhotoAction(string fileId, string? caption, bool isReply) : base(isReply)
    {
        _fileId = fileId;
        _caption = caption == string.Empty ? null : caption;
    }

    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendPhotoMessage(receiverInfo, _fileId, _caption, _isReply);
    }
}