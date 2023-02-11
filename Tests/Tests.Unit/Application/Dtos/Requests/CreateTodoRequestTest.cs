using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class CreateTodoRequestTest
    {
        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData("Ir ao mercado.")]
        [InlineData("Ir ao Dentista.")]
        [InlineData("Fazer investimentos.")]
        [InlineData("Pagar as contas.")]
        public void ShouldExecuteSuccessfully(string title)
        {
            // Arranje
            CreateTodoRequest createTodoRequest;

            // Act
            createTodoRequest = new CreateTodoRequest(title);

            // Assert
            createTodoRequest.Should().NotBeNull();
            createTodoRequest.Title.Should().Be(title);
        }
    }
}