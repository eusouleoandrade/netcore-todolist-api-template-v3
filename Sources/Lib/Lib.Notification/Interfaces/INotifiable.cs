using Lib.Notification.Models;

namespace Lib.Notification.Interfaces
{
    public interface INotifiable<TNotificationMessage>
        where TNotificationMessage : class
    {
        // Properties
        bool HasErrorNotification { get; }

        bool HasSuccessNotification { get; }

        IReadOnlyList<TNotificationMessage> ErrorNotifications { get; }

        IReadOnlyList<TNotificationMessage> SuccessNotifications { get; }
    }

    public interface INotifiable : INotifiable<NotificationMessage>
    {
    }
}