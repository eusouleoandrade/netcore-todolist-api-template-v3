using System.Diagnostics.CodeAnalysis;
using Lib.Notification.Abstractions;

namespace Lib.Notification.Contexts
{
    [ExcludeFromCodeCoverage]
    public class NotificationContext : Notifiable
    {
    }

    [ExcludeFromCodeCoverage]
    public class NotificationContext<TNotificationMessage> : Notifiable<TNotificationMessage>
        where TNotificationMessage : class
    {
    }
}