using AutoMapper;
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
    public class DeleteTodoUseCaseTest
    {
        private readonly IMapper _mapperMock;

        private readonly Mock<IGenericRepositoryAsync<Todo, int>> _genericRepositoryAsyncMock;

        private readonly Mock<IGenericRepositoryAsync<Todo, int>> _getGenericRepositoryAsyncMock;
        
        private readonly Mock<IGetTodoUseCase> _getTodoUseCaseMock;

        private readonly Mock<ILogger<DeleteTodoUseCase>> _loggerMock;

        private readonly Mock<ILogger<GetTodoUseCase>> _loggerGetTodoUseCaseMock;

        public DeleteTodoUseCaseTest()
        {
            // Repository mock
            _genericRepositoryAsyncMock = new Mock<IGenericRepositoryAsync<Todo, int>>();
            _getGenericRepositoryAsyncMock = new Mock<IGenericRepositoryAsync<Todo, int>>();

            // UseCase mock
            _getTodoUseCaseMock = new Mock<IGetTodoUseCase>();

            // Logger mock
            _loggerMock = new Mock<ILogger<DeleteTodoUseCase>>();
            _loggerGetTodoUseCaseMock = new Mock<ILogger<GetTodoUseCase>>();

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
        public async Task ShouldExecuteSucessfully(int id, string title, bool done)
        {
            // Arranje
            var todoUseCaseResponse = new TodoUseCaseResponse(id, title, done);
            _getTodoUseCaseMock.Setup(x => x.RunAsync(It.IsAny<int>())).ReturnsAsync(todoUseCaseResponse);

            var deleteGenericRepositoryAsyncResponse = true;
            _genericRepositoryAsyncMock.Setup(x => x.DeleteAsync(It.IsAny<Todo>())).ReturnsAsync(deleteGenericRepositoryAsyncResponse);

            var deleteTodoUseCase = new DeleteTodoUseCase(_genericRepositoryAsyncMock.Object, _getTodoUseCaseMock.Object, _mapperMock, _loggerMock.Object);

            // Act
            var deleteUseCaseResponse = await deleteTodoUseCase.RunAsync(id);

            // Assert
            deleteUseCaseResponse.Should().BeTrue();

            deleteTodoUseCase.HasErrorNotification.Should().BeFalse();
            deleteTodoUseCase.ErrorNotifications.Should().HaveCount(0);
            deleteTodoUseCase.ErrorNotifications.Should().BeEmpty();

            _loggerMock
                .VerifyLogger("Start useCase DeleteTodoUseCase > method RunAsync.", LogLevel.Information)
                .VerifyLogger("Finishes successfully useCase DeleteTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute successfully when failed to remove
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        [InlineData(1, "Ir ao mercado.", true)]
        [InlineData(2, "Ir ao Dentista.", false)]
        [InlineData(3, "Fazer investimentos.", true)]
        [InlineData(4, "Pagar as contas.", false)]
        [Theory(DisplayName = "Should not execute successfully when failed to remove")]
        public async Task ShouldNotExecute_WhenFailedToRemove(int id, string title, bool done)
        {
            // Arranje
            var todoUseCaseResponse = new TodoUseCaseResponse(id, title, done);
            _getTodoUseCaseMock.Setup(x => x.RunAsync(It.IsAny<int>())).ReturnsAsync(todoUseCaseResponse);

            var deleteGenericRepositoryAsyncResponse = false;
            _genericRepositoryAsyncMock.Setup(x => x.DeleteAsync(It.IsAny<Todo>())).ReturnsAsync(deleteGenericRepositoryAsyncResponse);

            var deleteTodoUseCase = new DeleteTodoUseCase(_genericRepositoryAsyncMock.Object, _getTodoUseCaseMock.Object, _mapperMock, _loggerMock.Object);

            // Act
            var deleteUseCaseResponse = await deleteTodoUseCase.RunAsync(id);

            // Assert
            deleteUseCaseResponse.Should().Be(default);

            deleteTodoUseCase.HasErrorNotification.Should().BeTrue();

            deleteTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            deleteTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            deleteTodoUseCase.ErrorNotifications.Should().ContainSingle();
            deleteTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0010" && e.Message == "Failed to remove Todo.");

            deleteTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase DeleteTodoUseCase > method RunAsync.", LogLevel.Information);
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

            var deleteTodoUseCase = new DeleteTodoUseCase(_genericRepositoryAsyncMock.Object, getTodoUseCase, _mapperMock, _loggerMock.Object);

            // Act
            var deleteTodoUseCaseResponse =  await deleteTodoUseCase.RunAsync(1);

            // Assert
            deleteTodoUseCaseResponse.Should().Be(default);

            deleteTodoUseCase.HasErrorNotification.Should().BeTrue();

            deleteTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            deleteTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            deleteTodoUseCase.ErrorNotifications.Should().ContainSingle();
            deleteTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0004" && e.Message == "Data of Todo 1 not found.");

            deleteTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase DeleteTodoUseCase > method RunAsync.", LogLevel.Information);
        }
    }
}