using DotNetFramework.Core.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;

namespace DotNetFramework.Core.Data
{
    public class SqlServerCompactDb
    {
        private readonly ILogger _logger;
        private readonly string _dbPath;
        private readonly string _connectionString;

        public SqlServerCompactDb(ILogger logger, string sqlCePath = null, string sqlCeConnectionString = null)
        {
            _logger = logger;
            _dbPath = sqlCePath ?? DefaultDbPath();
            _connectionString = sqlCeConnectionString ?? DefaultConnectionString();
        }

        /// <summary>
        /// Return true if no new database was created.
        /// </summary>
        public bool EnsureCreated()
        {
            try
            {
                if (!File.Exists(_dbPath))
                {
                    using (SqlCeEngine engine = new(_connectionString))
                    {
                        engine.CreateDatabase();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to verify existence of SQL Server Compact database or to create one.");
            }

            return true;
        }

        public void ExecuteQuery(string sqlQuery)
        {
            ExecuteQueries([sqlQuery]);
        }

        public void ExecuteQueries(IEnumerable<string> sqlQueries)
        {
            try
            {
                using (SqlCeConnection sqlConnection = new(_connectionString))
                {
                    sqlConnection.Open();

                    using (SqlCeCommand sqlCommand = new())
                    {
                        sqlCommand.Connection = sqlConnection;

                        foreach (string sqlQuery in sqlQueries)
                        {
                            sqlCommand.CommandText = sqlQuery;
                            sqlCommand.ExecuteNonQuery();
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to execute SQL query.");
            }
        }

        private static string DefaultDbPath()
        {
            string appFolderPath = IO.GetAppDirectoryPath();
            string appName = new DirectoryInfo(appFolderPath).Name;
            string dbPath = Path.Combine(appFolderPath, $"{appName}.sdf");

            return dbPath;
        }

        private static string DefaultConnectionString()
        {
            //TODO: Have to test that this connection works, and not just in debug. All the examples I see use |DataDirectory|, like the following:
            //      https://github.dev/zzzprojects/EntityFramework-Classic/blob/4953e0478771ec1b065503a12a8bbef132569492/test/Shared/FunctionalTests/SqlServerCompact/CodePlex2197.cs#L83#L120

            string dbPath = DefaultDbPath();
            string connectionString = $"Data Source=\"{dbPath}\";";

            return connectionString;
        }
    }
}
