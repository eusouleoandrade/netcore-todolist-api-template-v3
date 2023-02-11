using Core.Application.Dtos.Responses;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Responses
{
    public class GetTodoUseCaseResponseTest
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
            GetTodoUseCaseResponse getTodoUseCaseResponse; 

            // Act
            getTodoUseCaseResponse = new GetTodoUseCaseResponse(id, title, done);

            // Assert
            getTodoUseCaseResponse.Should().NotBeNull();
            getTodoUseCaseResponse.Id.Should().Be(id);
            getTodoUseCaseResponse.Title.Should().Be(title);
            getTodoUseCaseResponse.Done.Should().Be(done);
        }
    }
}