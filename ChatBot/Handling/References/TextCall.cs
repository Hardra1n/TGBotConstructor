public class TextCall : Call
{
    private string _callValue;

    public TextCall(string callValue) : base(CallMethod.Text)
    {
        _callValue = callValue;
    }

    public override bool IsMyKey(object obj)
        => _callValue == (string)obj;
}