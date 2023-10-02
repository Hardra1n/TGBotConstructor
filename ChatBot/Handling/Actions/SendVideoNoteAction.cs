using ChatBot.Model;

namespace ChatBot.Handling.Actions;

public class SendVideoNoteAction : SendBotAction
{
    private readonly string _fileId;

    public SendVideoNoteAction(string fileId, bool isReply) : base(isReply)
    {
        _fileId = fileId;
    }

    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendVideoNoteMessage(receiverInfo, _fileId, _isReply);
    }
}
