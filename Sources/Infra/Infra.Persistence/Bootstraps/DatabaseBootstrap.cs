using System.Diagnostics.CodeAnalysis;
using Core.Application.Exceptions;
using Core.Application.Resources;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence.Bootstraps
{
    [ExcludeFromCodeCoverage]
    public class DatabaseBootstrap
    {
        private readonly IConfiguration _configuration;

        public DatabaseBootstrap(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Setup()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            var connection = new SqliteConnection(connectionString);

            try
            {
                // Insert the scripts here to create database and data objects at application startup.
                TodoBootstrap.Execute(connection);
            }
            catch (System.Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}