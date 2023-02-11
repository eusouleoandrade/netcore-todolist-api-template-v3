using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence.Configs
{
    [ExcludeFromCodeCoverage]
    public abstract class ConnectionConfig : IDisposable
    {
        protected readonly SqliteConnection _connection;

        protected ConnectionConfig(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqliteConnection(connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _connection?.Dispose();
        }
    }
}