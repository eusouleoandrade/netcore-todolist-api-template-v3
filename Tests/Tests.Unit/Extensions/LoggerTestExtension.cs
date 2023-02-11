using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Unit.Extensions
{
    public static class LoggerTestExtension
    {
        public static Mock<ILogger<T>> VerifyLogger<T>(this Mock<ILogger<T>> logger, string expectedMessage, LogLevel expectedLogLevel = LogLevel.Debug, int times = 1)
        {
            Func<object, Type, bool> state = (v, t) => v.ToString().CompareTo(expectedMessage) == 0;

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Exactly(times));

            return logger;
        } 
    }
}