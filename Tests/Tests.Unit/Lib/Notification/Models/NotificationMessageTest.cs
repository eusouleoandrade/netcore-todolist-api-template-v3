using FluentAssertions;
using Lib.Notification.Models;

namespace Tests.Unit.Lib.Notification.Models
{
    public class NotificationMessageTest
    {
        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message"></param>
        [Theory(DisplayName="Should execute successfully")]
        [InlineData("COD001", "Message 01")]
        [InlineData("COD002", "Message 02")]
        public void ShouldExecuteSuccessfully(string key, string message)
        {
            // Arranje
            NotificationMessage notificationMessage;

            // Act
            notificationMessage = new NotificationMessage(key, message);

            // Assert
            notificationMessage.Should().NotBeNull();
            notificationMessage.Key.Should().Be(key);
            notificationMessage.Message.Should().Be(message);
        }
    }
}