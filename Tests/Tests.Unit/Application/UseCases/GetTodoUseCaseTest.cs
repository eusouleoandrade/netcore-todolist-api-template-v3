using AutoMapper;
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
    public class GetTodoUseCaseTest
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IGenericRepositoryAsync<Todo, int>> _genericRepositoryAsyncMock;
        private readonly Mock<ILogger<GetTodoUseCase>> _loggerMock;

        public GetTodoUseCaseTest()
        {
            // Repository mock
            _genericRepositoryAsyncMock = new Mock<IGenericRepositoryAsync<Todo, int>>();

            // Logger mock
            _loggerMock = new Mock<ILogger<GetTodoUseCase>>();

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
        [InlineData(1, "Ir ao mercado.", false)]
        [InlineData(1, "Fazer investimentos.", true)]
        [InlineData(1, "Fazer atividade física.", false)]
        [InlineData(1, "Pagar as contas do mês.", true)]
        public async Task ShouldExecuteSucessfully(int id, string title, bool done)
        {
            // Arranje
            var todo = new Todo(id, title, done);

            _genericRepositoryAsyncMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(todo);

            var getTodoUseCase = new GetTodoUseCase(_genericRepositoryAsyncMock.Object, _mapperMock, _loggerMock.Object);

            // Act
            var getTodoUseCaseResponse = await getTodoUseCase.RunAsync(id);

            // Assert
            getTodoUseCaseResponse.Should().NotBeNull();
            getTodoUseCaseResponse.Should().BeEquivalentTo(todo);

            getTodoUseCase.Should().NotBeNull();

            getTodoUseCase.HasErrorNotification.Should().BeFalse();

            getTodoUseCase.ErrorNotifications.Should().BeEmpty();
            getTodoUseCase.ErrorNotifications.Should().HaveCount(0);

            _loggerMock
                .VerifyLogger("Start useCase GetTodoUseCase > method RunAsync.", LogLevel.Information)
                .VerifyLogger("Finishes successfully useCase GetTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute when id is invalid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should not execute when id is invalid")]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldNotExecute_WhenIdIsInvalid(int id)
        {
            // Arranje
            var getTodoUseCase = new GetTodoUseCase(_genericRepositoryAsyncMock.Object, _mapperMock, _loggerMock.Object);

            // Act
            var useCaseResponse = await getTodoUseCase.RunAsync(id);

            // Assert
            useCaseResponse.Should().BeNull();
            useCaseResponse.Should().Be(default);

            getTodoUseCase.HasErrorNotification.Should().BeTrue();

            getTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            getTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            getTodoUseCase.ErrorNotifications.Should().ContainSingle();
            getTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0005" && e.Message == $"Identifier {id} is invalid.");

            getTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase GetTodoUseCase > method RunAsync.", LogLevel.Information);
        }

        /// <summary>
        /// Should not execute when todo is null
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Should not execute when todo is null")]
        public async Task ShouldNotExecute_WhenTodoIsNull()
        {
            // Arranje
            var getTodoUseCase = new GetTodoUseCase(_genericRepositoryAsyncMock.Object, _mapperMock, _loggerMock.Object);
            var idRandom = new Random().Next(1, 100);

            // Act
            var useCaseResponse = await getTodoUseCase.RunAsync(idRandom);

            // Assert
            useCaseResponse.Should().BeNull();
            useCaseResponse.Should().Be(default);

            getTodoUseCase.HasErrorNotification.Should().BeTrue();

            getTodoUseCase.ErrorNotifications.Should().NotBeEmpty();
            getTodoUseCase.ErrorNotifications.Should().HaveCount(1);
            getTodoUseCase.ErrorNotifications.Should().ContainSingle();
            getTodoUseCase.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0004" && e.Message == $"Data of Todo {idRandom} not found.");

            getTodoUseCase.SuccessNotifications.Should().BeEmpty();

            _loggerMock.VerifyLogger("Start useCase GetTodoUseCase > method RunAsync.", LogLevel.Information);
        }
    }
}