using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class CreateTodoUseCaseRequestTest
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
            CreateTodoUseCaseRequest createTodoUseCaseRequest;

            // Act
            createTodoUseCaseRequest = new CreateTodoUseCaseRequest(title);

            // Assert
            createTodoUseCaseRequest.Should().NotBeNull();
            createTodoUseCaseRequest.Title.Should().Be(title);
            createTodoUseCaseRequest.Done.Should().Be(false);
        }

        /// <summary>
        /// Should not execute when title is invalid
        /// </summary>
        /// <param name="title"></param>
        [Theory(DisplayName = "Should not execute when title is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        public void ShouldNotExecute_WhenTitleIsInvalid(string title)
        {
            // Arranje
            CreateTodoUseCaseRequest createTodoUseCaseRequest;

            // Act
            createTodoUseCaseRequest = new CreateTodoUseCaseRequest(title);

            // Assert
            createTodoUseCaseRequest.Should().NotBeNull();

            createTodoUseCaseRequest.HasErrorNotification.Should().BeTrue();

            createTodoUseCaseRequest.ErrorNotifications.Should().NotBeEmpty();
            createTodoUseCaseRequest.ErrorNotifications.Should().HaveCount(1);
            createTodoUseCaseRequest.ErrorNotifications.Should().ContainSingle();
            createTodoUseCaseRequest.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0002" && e.Message == "Title is required.");

            createTodoUseCaseRequest.SuccessNotifications.Should().BeEmpty();
        }
    }
}