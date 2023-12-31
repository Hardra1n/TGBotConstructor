using ChatBot.Handling.Actions;
using ChatBot.Model;

public class SendVideoAction : SendBotAction
{
    private readonly string _fileId;
    private readonly string? _caption;

    public SendVideoAction(string fileId, string? caption, bool isReply) : base(isReply)
    {
        _fileId = fileId;
        _caption = caption == string.Empty ? null : caption;
    }

    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendVideoMessage(receiverInfo, _fileId, _caption, _isReply);
    }
}