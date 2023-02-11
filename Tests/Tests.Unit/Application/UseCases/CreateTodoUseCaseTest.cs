using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Interfaces.Repositories;
using Core.Application.Mappings;
using Core.Application.UseCases;
using Core.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Unit.Extensions;

namespace Tests.Unit.Application.UseCases
{
    public class CreateTodoUseCaseTest
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<ITodoRepositoryAsync> _todoRepositoryAsyncMock;

        private readonly Mock<ILogger<CreateTodoUseCase>> _loggerMock;

        public CreateTodoUseCaseTest()
        {
            // Repository mock
            _todoRepositoryAsyncMock = new Mock<ITodoRepositoryAsync>();

            // Logger mock
            _loggerMock = new Mock<ILogger<CreateTodoUseCase>>();

            // Set auto mapper configs
            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, "Ir ao mercado.")]
        [InlineData(2, "Ir ao Dentista.")]
        [InlineData(3, "Fazer investimentos.")]
        [InlineData(4, "Pagar as contas.")]
        public async Task ShouldExecuteSucessfully(int id, string title)
        {
            // Arranje
            var todo = new Todo(id, title, false);
            _todoRepositoryAsyncMock.Setup(x => x.CreateAsync(It.IsAny<Todo>())).ReturnsAsync(todo);

            var createTodoUseCase = new CreateTodoUseCase(_mapperMock, _todoRepositoryAsyncMock.Object, _loggerMock.Object);
            var useCaseRequest = new CreateTodoUseCaseRequest(title);

            // Act
            var useCaseResponse = await createTodoUseCase.RunAsync(useCaseRequest);

            // Assert
            useCaseResponse.Should().NotBeNull();
            useCaseResponse.Should().BeEquivalentTo(todo);

            createTodoUseCase.HasErrorNotification.Should().BeFalse();
            createTodoUseCase.ErrorNotifications.Should().HaveCount(0);
            createTodoUseCase.ErrorNotifications.Should().BeEmpty();

            _loggerMock
                .VerifyLogger("Start useCase CreateTodoUseCase > method RunAsync.", LogLevel.Information)
                .VerifyLogger("Finishes successfully useCase CreateTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute when title is invalid
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should not execute when title is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldNotExecute_WhenTitleIsInvalid(string title)
        {
            // Arranje
            var createTodoUseCase = new CreateTodoUseCase(_mapperMock, _todoRepositoryAsyncMock.Object, _loggerMock.Object);
            var useCaseRequest = new CreateTodoUseCaseRequest(title);

            // Act
            var useCaseResponse = await createTodoUseCase.RunAsync(useCaseRequest);

            // Assert
            useCaseResponse.Should().BeNull();
            useCaseResponse.Should().Be(default);

            createTodoUseCase.HasErrorNotification.Should().BeTrue();

            createTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            createTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            createTodoUseCase.ErrorNotifications.Should().ContainSingle();
            createTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0002" && e.Message == "Title is required.");

            createTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase CreateTodoUseCase > method RunAsync.", LogLevel.Information);
        }
    }
}