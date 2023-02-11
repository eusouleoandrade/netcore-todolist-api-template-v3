using Core.Application.Dtos.Responses;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Responses
{
    public class CreateTodoUseCaseResponseTest
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
            CreateTodoUseCaseResponse createTodoUseCaseResponse; 

            // Act
            createTodoUseCaseResponse = new CreateTodoUseCaseResponse(id, title, done);

            // Assert
            createTodoUseCaseResponse.Should().NotBeNull();
            createTodoUseCaseResponse.Id.Should().Be(id);
            createTodoUseCaseResponse.Title.Should().Be(title);
            createTodoUseCaseResponse.Done.Should().Be(done);
        }
    }
}