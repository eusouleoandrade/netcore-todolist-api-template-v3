using Core.Application.Dtos.Responses;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Responses
{
    public class TodoUseCaseResponseTest
    {
        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, "Ir ao mercado.", true)]
        [InlineData(2, "Ir ao Dentista.", false)]
        [InlineData(3, "Fazer investimentos.", true)]
        [InlineData(4, "Pagar as contas.", false)]
        public void ShouldExecuteSuccessfully(int id, string title, bool done)
        {
            // Arranje
            TodoUseCaseResponse todoUseCaseResponse; 

            // Act
            todoUseCaseResponse = new TodoUseCaseResponse(id, title, done);

            // Assert
            todoUseCaseResponse.Should().NotBeNull();
            todoUseCaseResponse.Id.Should().Be(id);
            todoUseCaseResponse.Title.Should().Be(title);
            todoUseCaseResponse.Done.Should().Be(done);
        }
    }
}