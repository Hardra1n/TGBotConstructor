namespace ChatBot.Model;
public class ReceiverInfo
{
    public ReceiverInfo(string name)
    {
        Name = name;
    }

    public long Id { get; set; }
    public string Name { get; set; }
}