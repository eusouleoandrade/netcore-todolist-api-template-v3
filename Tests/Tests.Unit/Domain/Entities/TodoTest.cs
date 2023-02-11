using Core.Domain.Entities;
using FluentAssertions;

namespace Tests.Unit.Domain.Entities
{
    public class TodoTest
    {
        /// <summary>
        /// Should execute successfully 1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should execute successfully 1")]
        [InlineData(1, "Ir ao mercado.", true)]
        [InlineData(2, "Ir ao Dentista.", false)]
        [InlineData(3, "Fazer investimentos.", true)]
        [InlineData(4, "Pagar as contas.", false)]
        public void ShouldExecuteSuccessfully1(int id, string title, bool done)
        {
            // Arranje
            Todo todo;

            // Act
            todo = new Todo(id, title, done);

            // Assert
            todo.Should().NotBeNull();
            todo.Id.Should().Be(id);
            todo.Title.Should().Be(title);
            todo.Done.Should().Be(done);
        }

        /// <summary>
        /// Should execute successfully 2
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should execute successfully 2")]
        [InlineData(1, "Ir ao mercado.", 1)]
        [InlineData(2, "Ir ao Dentista.", 0)]
        [InlineData(3, "Fazer investimentos.", 1)]
        [InlineData(4, "Pagar as contas.", 0)]
        public void ShouldExecuteSuccessfully2(Int64 id, string title, Int64 done)
        {
            // Arranje
            Todo todo;

            // Act
            todo = new Todo(id, title, done);

            // Assert
            todo.Should().NotBeNull();
            todo.Id.Should().Be((int)id);
            todo.Title.Should().Be(title);

            if (done == decimal.Zero)
                todo.Done.Should().BeFalse();
            else
                todo.Done.Should().BeTrue();
        }

        /// <summary>
        /// Should execute successfully 3
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should execute successfully 3")]
        [InlineData(1, "Ir ao mercado.", "True")]
        [InlineData(2, "Ir ao Dentista.", "False")]
        [InlineData(3, "Fazer investimentos.", "True")]
        [InlineData(4, "Pagar as contas.", "False")]
        public void ShouldExecuteSuccessfully3(Int64 id, string title, string done)
        {
            // Arranje
            Todo todo;

            // Act
            todo = new Todo(id, title, done);

            // Assert
            todo.Should().NotBeNull();
            todo.Id.Should().Be((int)id);
            todo.Title.Should().Be(title);
            todo.Done.Should().Be(Convert.ToBoolean(done));
        }
    }
}