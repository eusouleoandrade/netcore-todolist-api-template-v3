namespace Core.Application.Interfaces.Services
{
    public interface ICorrelationIdService
    {
        string GetCorrelationId();

        void SetCorrelationId(string correlationId);
    }
}