using Core.Application.Dtos.Requests;
using FluentAssertions;

namespace Tests.Unit.Application.Dtos.Requests
{
    public class SetDoneTodoRequestTest
    {
        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="done"></param>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(true)]
        [InlineData(false)]
        public void ShouldExecuteSuccessfully(bool done)
        {
            // Arranje
            SetDoneTodoRequest setDoneTodoRequest;

            // Act
            setDoneTodoRequest = new SetDoneTodoRequest(done);

            // Assert
            setDoneTodoRequest.Should().NotBeNull();
            setDoneTodoRequest.Done.Should().Be(done);
        }
    }
}