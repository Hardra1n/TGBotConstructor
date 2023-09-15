namespace ChatBot.Model;
public class ReceiverInfo
{
    public ReceiverInfo(string name, string messageId)
    {
        Name = name;
        MessageId = messageId;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string MessageId { get; set; }
}