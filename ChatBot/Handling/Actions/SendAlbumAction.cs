using ChatBot.Model;

namespace ChatBot.Handling.Actions;

public class SendAlbumAction : SendBotAction
{
    private IEnumerable<KeyValuePair<string, string>> _typeFileIdPairs;

    public SendAlbumAction(IEnumerable<KeyValuePair<string, string>> typeFileIdPairs, bool isReply) : base(isReply)
    {
        _typeFileIdPairs = typeFileIdPairs;
    }

    public override async Task Execute(IChatBot chatBot, ReceiverInfo receiverInfo)
    {
        await chatBot.SendAlbumMessage(receiverInfo, _typeFileIdPairs, _isReply);
    }
}
