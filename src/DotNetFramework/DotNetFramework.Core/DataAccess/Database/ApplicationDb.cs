using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;

namespace DotNetFramework.Core.DataAccess
{
    public class ApplicationDb
    {
        public void EnsureCreated()
        {
            string connectionString = ConnectionString();

            if (!File.Exists(connectionString))
            {
                using (SqlCeEngine engine = new(connectionString))
                {
                    engine.CreateDatabase();
                }
            }
        }

        public void ExecuteQueries(IEnumerable<string> queries)
        {
            using (SqlCeConnection sqlConnection = new(ConnectionString()))
            {
                sqlConnection.Open();

                using (SqlCeCommand sqlCommand = new())
                {
                    sqlCommand.Connection = sqlConnection;

                    foreach (string query in queries)
                    {
                        sqlCommand.CommandText = query;
                        sqlCommand.ExecuteNonQuery();
                    }
                }

                sqlConnection.Close();
            }
        }

        private static string ConnectionString()
        {
            //TODO: Have to test that this connection works, and not just in debug. All the examples I see use |DataDirectory|, like the following:
            //      https://github.dev/zzzprojects/EntityFramework-Classic/blob/4953e0478771ec1b065503a12a8bbef132569492/test/Shared/FunctionalTests/SqlServerCompact/CodePlex2197.cs#L83#L120
            string appFolderPath = IO.GetAppDirectoryPath();
            string appName = new DirectoryInfo(appFolderPath).Name;
            string dbPath = Path.Combine(appFolderPath, $"{appName}.sdf");

            return $"Data Source=\"{dbPath}\";";
        }
    }
}
