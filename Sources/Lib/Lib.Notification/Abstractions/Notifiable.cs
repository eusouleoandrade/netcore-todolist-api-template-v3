using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Lib.Notification.Interfaces;
using Lib.Notification.Models;

namespace Lib.Notification.Abstractions
{
    [ExcludeFromCodeCoverage]
    public abstract class Notifiable : Notifiable<NotificationMessage>, INotifiable
    {
        // Error notifications
        public void AddErrorNotification(string key, string message)
        {
            _errorNotifications.Add(new NotificationMessage(key, message));
        }

        public void AddErrorNotification(string key, string message, params object[] parameters)
            => _errorNotifications.Add(new NotificationMessage(key, string.Format(message, parameters)));

        public void AddErrorNotifications(params INotifiable[] objects)
        {
            foreach (INotifiable notifiable in objects)
                _errorNotifications.AddRange(notifiable.ErrorNotifications);
        }

        // Success notifications
        public void AddSuccessNotification(string key, string message)
            => _successNotifications.Add(new NotificationMessage(key, message));

        public void AddSuccessNotification(string key, string message, params object[] parameters)
            => _successNotifications.Add(new NotificationMessage(key, string.Format(message, parameters)));

        public void AddSuccessNotifications(params INotifiable[] objects)
        {
            foreach (INotifiable notifiable in objects)
                _successNotifications.AddRange(notifiable.ErrorNotifications);
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class Notifiable<TNotificationMessage> : INotifiable<TNotificationMessage>, IDisposable
            where TNotificationMessage : class
    {
        protected readonly List<TNotificationMessage> _errorNotifications;

        protected readonly List<TNotificationMessage> _successNotifications;

        [NotMapped]
        [JsonIgnore]
        public bool HasErrorNotification => _errorNotifications.Any();

        [NotMapped]
        [JsonIgnore]
        public bool HasSuccessNotification => _successNotifications.Any();

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<TNotificationMessage> ErrorNotifications => _errorNotifications;

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyList<TNotificationMessage> SuccessNotifications => _successNotifications;

        protected Notifiable()
        {
            _errorNotifications = new List<TNotificationMessage>();
            _successNotifications = new List<TNotificationMessage>();
        }

        // Error notifications
        public void AddErrorNotification(TNotificationMessage notification)
            => _errorNotifications.Add(notification);

        public void AddErrorNotifications(IEnumerable<TNotificationMessage> notifications)
        {
            if (notifications.Any())
                _errorNotifications.AddRange(notifications);
        }

        // Success notifications
        public void AddSuccessNotification(TNotificationMessage notification)
            => _successNotifications.Add(notification);

        public void AddSuccessNotifications(IEnumerable<TNotificationMessage> notifications)
        {
            if (notifications.Any())
                _successNotifications.AddRange(notifications);
        }

        // Clear notifications
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _errorNotifications.Clear();
            _successNotifications.Clear();
        }
    }
}