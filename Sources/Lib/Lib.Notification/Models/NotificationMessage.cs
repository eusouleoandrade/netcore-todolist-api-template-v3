using System.Text.Json.Serialization;

namespace Lib.Notification.Models
{
    public class NotificationMessage
    {
        [JsonPropertyName("cod")]
        public string Key { get; set; }

        public string Message { get; set; }

        public NotificationMessage(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}