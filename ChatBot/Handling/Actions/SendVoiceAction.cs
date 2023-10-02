using ChatBot.Model;

namespace ChatBot.Handling.Actions;

public class SendVoiceAction : SendBotAction
{
    private readonly string _fileId;
    private readonly string? _caption;

    public SendVoiceAction(string fileId, string? caption, bool isReply) : base(isReply)
    {
        _fileId = fileId;
        _caption = caption == string.Empty ? null : caption;
    }
    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendVoiceMessage(receiverInfo, _fileId, _caption, _isReply);
    }
}