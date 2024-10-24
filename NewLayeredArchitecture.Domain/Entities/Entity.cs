using NewLayeredArchitecture.Application.Notifications;

namespace NewLayeredArchitecture.Domain.Entities;

public abstract class Entity
{
    private List<Notification> _notifications = new List<Notification>();

    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    protected void AddNotification(string message, string key)
    {
        _notifications.Add(new Notification(key, message));
    }

    protected void ClearNotifications()
    {
        _notifications.Clear();
    }

    public bool IsValid => !_notifications.Any();
}

