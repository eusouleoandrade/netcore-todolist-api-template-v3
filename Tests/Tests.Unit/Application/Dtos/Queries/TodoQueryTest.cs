using Core.Application.Dtos.Queries;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Queries
{
    public class TodoQueryTest
    {
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
        public void ShouldExecuteSuccessfully(int id, string title, bool done)
        {
            // Arranje
            TodoQuery todoQuery;

            // Act
            todoQuery = new TodoQuery(id, title, done);

            // Assert
            todoQuery.Should().NotBeNull();
            todoQuery.Id.Should().Be(id);
            todoQuery.Title.Should().Be(title);
            todoQuery.Done.Should().Be(done);
        }
    }
}