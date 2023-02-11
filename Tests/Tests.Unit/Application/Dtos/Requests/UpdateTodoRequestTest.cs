using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class UpdateTodoRequestTest
    {
        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="title"></param>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData("Ir ao mercado.", true)]
        [InlineData("Ir ao Dentista.", false)]
        public void ShouldExecuteSuccessfully(string title, bool done)
        {
            // Arranje
            UpdateTodoRequest updateTodoRequest;

            // Act
            updateTodoRequest = new UpdateTodoRequest(title, done);

            // Assert
            updateTodoRequest.Should().NotBeNull();
            updateTodoRequest.Title.Should().Be(title);
            updateTodoRequest.Done.Should().Be(done);
        }
    }
}