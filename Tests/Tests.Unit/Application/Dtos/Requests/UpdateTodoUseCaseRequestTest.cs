using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class UpdateTodoUseCaseRequestTest
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
            UpdateTodoUseCaseRequest updateTodoUseCaseRequest;

            // Act
            updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(id, title, done);

            // Assert
            updateTodoUseCaseRequest.Should().NotBeNull();
            updateTodoUseCaseRequest.Id.Should().Be(id);
            updateTodoUseCaseRequest.Title.Should().Be(title);
            updateTodoUseCaseRequest.Done.Should().Be(done);
        }

        /// <summary>
        /// Should not execute when id is invalid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should not execute when id is invalid")]
        [InlineData(0, "Ir ao mercado.", true)]
        [InlineData(-1, "Ir ao Dentista.", false)]
        public void ShouldNotExecute_WhenIdIsInvalid(int id, string title, bool done)
        {
            // Arranje
            UpdateTodoUseCaseRequest updateTodoUseCaseRequest;

            // Act
            updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(id, title, done);

            // Assert
            updateTodoUseCaseRequest.Should().NotBeNull();

            updateTodoUseCaseRequest.HasErrorNotification.Should().BeTrue();

            updateTodoUseCaseRequest.ErrorNotifications.Should().NotBeEmpty();
            updateTodoUseCaseRequest.ErrorNotifications.Should().HaveCount(1);
            updateTodoUseCaseRequest.ErrorNotifications.Should().ContainSingle();
            updateTodoUseCaseRequest.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0005" && e.Message == $"Identifier {id} is invalid.");

            updateTodoUseCaseRequest.SuccessNotifications.Should().BeEmpty();
        }

        /// <summary>
        /// Should not execute when title is invalid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should not execute when title is invalid")]
        [InlineData(1, "", true)]
        [InlineData(2, " ", false)]
        public void ShouldNotExecute_WhenTitleIsInvalid(int id, string title, bool done)
        {
            // Arranje
            UpdateTodoUseCaseRequest updateTodoUseCaseRequest;

            // Act
            updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(id, title, done);

            // Assert
            updateTodoUseCaseRequest.Should().NotBeNull();

            updateTodoUseCaseRequest.HasErrorNotification.Should().BeTrue();

            updateTodoUseCaseRequest.ErrorNotifications.Should().NotBeEmpty();
            updateTodoUseCaseRequest.ErrorNotifications.Should().HaveCount(1);
            updateTodoUseCaseRequest.ErrorNotifications.Should().ContainSingle();
            updateTodoUseCaseRequest.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0002" && e.Message == "Title is required.");

            updateTodoUseCaseRequest.SuccessNotifications.Should().BeEmpty();
        }
    }
}