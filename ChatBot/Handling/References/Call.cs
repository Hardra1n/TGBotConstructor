using System.Collections.Concurrent;

public abstract class Call
{
    public Call(CallMethod invokeMethod)
    {
        InvokeMethod = invokeMethod;
    }

    public CallMethod InvokeMethod { get; private set; }

    public abstract bool IsMyKey(object obj);
}