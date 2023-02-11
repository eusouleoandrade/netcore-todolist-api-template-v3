using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class SetDoneTodoUseCaseRequestTest
    {
        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="id"></param>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public void ShouldExecuteSuccessfully(int id, bool done)
        {
            // Arranje
            SetDoneTodoUseCaseRequest setDoneTodoUseCaseRequest;

            // Act
            setDoneTodoUseCaseRequest = new SetDoneTodoUseCaseRequest(id, done);

            // Assert
            setDoneTodoUseCaseRequest.Should().NotBeNull();
            setDoneTodoUseCaseRequest.Id.Should().Be(id);
            setDoneTodoUseCaseRequest.Done.Should().Be(done);
        }
    }
}