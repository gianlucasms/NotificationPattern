namespace NewLayeredArchitecture.Application.Notifications;

public interface INotificationHandler
{
    void AddNotification(string key, string message);
    IEnumerable<Notification> GetNotifications();
    bool HasNotifications();
}

public class NotificationHandler : INotificationHandler
{
    private readonly List<Notification> _notifications = new List<Notification>();

    public void AddNotification(string key, string message)
    {
        _notifications.Add(new Notification(key, message));
    }

    public IEnumerable<Notification> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotifications()
    {
        return _notifications.Any();
    }
}

