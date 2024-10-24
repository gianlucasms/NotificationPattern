namespace NewLayeredArchitecture.Application.Notifications;

public class Notification
{
    public string Message { get; }
    public string Key { get; }  

    public Notification(string key, string message)
    {
        Message = message;
        Key = key;
    }
}

