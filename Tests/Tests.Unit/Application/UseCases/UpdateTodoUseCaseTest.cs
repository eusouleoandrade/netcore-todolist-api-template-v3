using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Application.Mappings;
using Core.Application.UseCases;
using Core.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Unit.Extensions;

namespace Tests.Unit.Application.UseCases
{
    public class UpdateTodoUseCaseTest
    {
        private readonly Mock<IGenericRepositoryAsync<Todo, int>> _genericRepositoryAsyncMock;
        private readonly Mock<IGenericRepositoryAsync<Todo, int>> _getGenericRepositoryAsyncMock;
        private readonly Mock<IGetTodoUseCase> _getTodoUseCaseMock;
        private readonly Mock<ILogger<UpdateTodoUseCase>> _loggerMock;
        private readonly Mock<ILogger<GetTodoUseCase>> _loggerGetTodoUseCaseMock;
        private readonly IMapper _mapperMock;

        public UpdateTodoUseCaseTest()
        {
            // Repository mock
            _genericRepositoryAsyncMock = new Mock<IGenericRepositoryAsync<Todo, int>>();
            _getGenericRepositoryAsyncMock = new Mock<IGenericRepositoryAsync<Todo, int>>();

            // Logger mock
            _loggerMock = new Mock<ILogger<UpdateTodoUseCase>>();
            _loggerGetTodoUseCaseMock = new Mock<ILogger<GetTodoUseCase>>();

            // UseCase mock
            _getTodoUseCaseMock = new Mock<IGetTodoUseCase>();

            // Set auto mapper configs
            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, "Ir ao mercado.", true)]
        [InlineData(2, "Ir ao Dentista.", false)]
        [InlineData(3, "Fazer investimentos.", true)]
        [InlineData(4, "Pagar as contas.", false)]
        public async Task ShouldExecuteSuccessfully(int id, string title, bool done)
        {
            // Arranje
            var todoUseCaseResponse = new TodoUseCaseResponse(id, title, done);
            _getTodoUseCaseMock.Setup(x => x.RunAsync(It.IsAny<int>())).ReturnsAsync(todoUseCaseResponse);

            var updateGenericRepositoryAsyncResponse = true;
            _genericRepositoryAsyncMock.Setup(x => x.UpdateAsync(It.IsAny<Todo>())).ReturnsAsync(updateGenericRepositoryAsyncResponse);

            var updateTodoUseCase = new UpdateTodoUseCase(_genericRepositoryAsyncMock.Object, _getTodoUseCaseMock.Object, _loggerMock.Object);

            var updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(id, $"{title} updated", !done);

            // Act
            var updateTodoUseCaseResponse = await updateTodoUseCase.RunAsync(updateTodoUseCaseRequest);

            // Assert
            updateTodoUseCaseResponse.Should().BeTrue();

            updateTodoUseCase.HasErrorNotification.Should().BeFalse();

            updateTodoUseCase.ErrorNotifications.Should().HaveCount(0);
            updateTodoUseCase.ErrorNotifications.Should().BeEmpty();

            _loggerMock
                .VerifyLogger("Start useCase UpdateTodoUseCase > method RunAsync.", LogLevel.Information)
                .VerifyLogger("Finishes successfully useCase UpdateTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute when id is invalid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should not execute when id is invalid")]
        [InlineData(0, "Ir ao mercado.", true)]
        [InlineData(-1, "Ir ao Dentista.", false)]
        [InlineData(-2, "Fazer investimentos.", true)]
        [InlineData(-3, "Pagar as contas.", false)]
        public async Task ShouldNotExecute_WhenIdIsInvalid(int id, string title, bool done)
        {
            // Arranje
            var updateTodoUseCase = new UpdateTodoUseCase(_genericRepositoryAsyncMock.Object, _getTodoUseCaseMock.Object, _loggerMock.Object);

            var updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(id, title, done);

            // Act
            var updateTodoUseCaseResponse = await updateTodoUseCase.RunAsync(updateTodoUseCaseRequest);

            // Assert
            updateTodoUseCaseResponse.Should().Be(default);

            updateTodoUseCase.HasErrorNotification.Should().BeTrue();

            updateTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            updateTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            updateTodoUseCase.ErrorNotifications.Should().ContainSingle();
            updateTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0005" && e.Message == $"Identifier {id} is invalid.");

            updateTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase UpdateTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute when title is invalid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should not execute when title is invalid")]
        [InlineData(1, " ", true)]
        [InlineData(2, "", true)]
        public async Task ShouldNotExecute_WhenTitleIsInvalid(int id, string title, bool done)
        {
            // Arranje
            var updateTodoUseCase = new UpdateTodoUseCase(_genericRepositoryAsyncMock.Object, _getTodoUseCaseMock.Object, _loggerMock.Object);

            var updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(id, title, done);

            // Act
            var updateTodoUseCaseResponse = await updateTodoUseCase.RunAsync(updateTodoUseCaseRequest);

            // Assert
            updateTodoUseCaseResponse.Should().Be(default);

            updateTodoUseCase.HasErrorNotification.Should().BeTrue();

            updateTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            updateTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            updateTodoUseCase.ErrorNotifications.Should().ContainSingle();
            updateTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0002" && e.Message == "Title is required.");

            updateTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase UpdateTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute when failed to update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should not execute when failed to update")]
        [InlineData(1, "Ir ao mercado.", true)]
        [InlineData(2, "Ir ao Dentista.", false)]
        [InlineData(3, "Fazer investimentos.", true)]
        [InlineData(4, "Pagar as contas.", false)]
        public async Task ShouldNotExecute_WhenFailedToUpdate(int id, string title, bool done)
        {
            // Arranje
            var updateGenericRepositoryAsyncResponse = false;
            _genericRepositoryAsyncMock.Setup(x => x.UpdateAsync(It.IsAny<Todo>())).ReturnsAsync(updateGenericRepositoryAsyncResponse);

            var todoUseCaseResponse = new TodoUseCaseResponse(id, title, done);
            _getTodoUseCaseMock.Setup(x => x.RunAsync(It.IsAny<int>())).ReturnsAsync(todoUseCaseResponse);

            var updateTodoUseCase = new UpdateTodoUseCase(_genericRepositoryAsyncMock.Object, _getTodoUseCaseMock.Object, _loggerMock.Object);

            var updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(id, $"{title} updated", !done);

            // Act
            var updateTodoUseCaseResponse = await updateTodoUseCase.RunAsync(updateTodoUseCaseRequest);

            // Assert
            updateTodoUseCaseResponse.Should().Be(default);

            updateTodoUseCase.HasErrorNotification.Should().BeTrue();

            updateTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            updateTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            updateTodoUseCase.ErrorNotifications.Should().ContainSingle();
            updateTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0006" && e.Message == "Failed to update Todo.");

            updateTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase UpdateTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute when not finding todo
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Should not execute when not finding todo")]
        public async Task ShouldNotExecute_WhenNotFindingTodo()
        {
            // Arranje
            _getGenericRepositoryAsyncMock.Setup(x => x.GetAsync(It.IsAny<int>()));

            IGetTodoUseCase getTodoUseCase = new GetTodoUseCase(_getGenericRepositoryAsyncMock.Object, _mapperMock, _loggerGetTodoUseCaseMock.Object);
            
            _ = await getTodoUseCase.RunAsync(1);

            var updateTodoUseCase = new UpdateTodoUseCase(_genericRepositoryAsyncMock.Object, getTodoUseCase, _loggerMock.Object);

            var updateTodoUseCaseRequest = new UpdateTodoUseCaseRequest(1, "todo updated", true);

            // Act
            var updateTodoUseCaseResponse =  await updateTodoUseCase.RunAsync(updateTodoUseCaseRequest);

            // Assert
            updateTodoUseCaseResponse.Should().Be(default);

            updateTodoUseCase.HasErrorNotification.Should().BeTrue();

            updateTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            updateTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            updateTodoUseCase.ErrorNotifications.Should().ContainSingle();
            updateTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0004" && e.Message == "Data of Todo 1 not found.");

            updateTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase UpdateTodoUseCase > method RunAsync.", LogLevel.Information);
        }
    }
}