using ChatBot.Model;

namespace ChatBot.Handling.Actions;

public class SendVideoNoteAction : BotAction
{
    private readonly string _fileId;

    public SendVideoNoteAction(string fileId)
    {
        _fileId = fileId;
    }

    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendVideoNoteMessage(receiverInfo, _fileId);
    }
}
