using System.Diagnostics.CodeAnalysis;
using Core.Application.Interfaces.Services;

namespace Infra.Shared.Services
{
    [ExcludeFromCodeCoverage]
    public class CorrelationIdService : ICorrelationIdService
    {
        private string _correlationId { get; set; }

        public CorrelationIdService()
        {
            _correlationId = Guid.NewGuid().ToString();
        }

        public string GetCorrelationId()
        {
            return _correlationId;
        }

        public void SetCorrelationId(string correlationId)
        {
            _correlationId = correlationId;
        }
    }
}