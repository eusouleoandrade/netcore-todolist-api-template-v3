using FluentAssertions;
using Lib.Notification.Extensions;

namespace Tests.Unit.Lib.Notification.Extensions
{
    public class StringExtensionTest
    {
        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData("Todo", 100)]
        [InlineData("Todo", 200)]
        public void ShouldExecuteSuccessfully(string entity, int id)
        {
            // Arranja
            string message = "A entidade {0} com o id {1} deve ser informada.";

            // Act
            string messageFormat = message.ToFormat(entity, id);

            // Assert
            messageFormat.Should().Be($"A entidade {entity} com o id {id} deve ser informada.");
            messageFormat.Should().NotBeNullOrEmpty();
            messageFormat.Should().NotBeNullOrWhiteSpace();
        }
    }
}