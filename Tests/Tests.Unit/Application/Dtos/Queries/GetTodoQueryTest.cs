using Core.Application.Dtos.Queries;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Queries
{
    public class GetTodoQueryTest
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
            GetTodoQuery getTodoQuery;

            // Act
            getTodoQuery = new GetTodoQuery(id, title, done);

            // Assert
            getTodoQuery.Should().NotBeNull();
            getTodoQuery.Id.Should().Be(id);
            getTodoQuery.Title.Should().Be(title);
            getTodoQuery.Done.Should().Be(done);
        }
    }
}