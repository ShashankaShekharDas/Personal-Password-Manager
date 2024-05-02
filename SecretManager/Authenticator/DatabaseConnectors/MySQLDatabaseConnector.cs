using MySql.Data.MySqlClient;
using SecretManager.Commons;
using SecretManager.Interfaces;
using SecretManager.Models.Database;
using System.Data.Common;

namespace SecretManager.Authenticator.DatabaseConnectors
{
    public sealed class MySqlDatabaseConnector<T>(string server, string username, string password, string database) 
        : AbstractDatabaseConnecter<T>(new DatabaseConnectionRecord { Server = server, UserName = username, Password = password }, database)
        where T : IDatabaseTableManager
    {
        private readonly string _connectionString = "server={server};uid={user};pwd={password};database={database}";

        protected override DbConnection CreateConnection()
        {
            var connectionString = _connectionString
                .Replace("{server}", _record.Server)
                .Replace("{user}", _record.UserName)
                .Replace("{password}", _record.Password)
                .Replace("{database}", _database);
            return new MySqlConnection(connectionString);
        }

        public override IEnumerable<T> GetData(string table, T tableData)
        {
            var conditions = tableData.GetColumnValuePairs();
            if (conditions.Count == 0) return [];

            var result = new List<T>();

            var whereConditionList = conditions.Select(condition => $"{condition[0]} = '{condition[1]}'");

            using var connection = CreateConnection();
            connection.Open();
            var command = new MySqlCommand
            {
                Connection = (MySqlConnection)connection,
                CommandText = SqlQueries.SELECT_QUERY
                                .Replace("{table}", table)
                                .Replace("{conditions}", string.Join(" AND ", whereConditionList))
            };

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add((T)T.GetRowFromReader(reader));
            }

            DestroyConnection(connection);
            return result;
        }

        public override int InsertData(string table, T tableData)
        {
            var dataToInsert = tableData.GetColumnValuePairs();
            if (dataToInsert.Count == 0) return 0;

            using var connection = CreateConnection();
            connection.Open();
            var command = new MySqlCommand
            {
                Connection = (MySqlConnection)connection,
                CommandText = SqlQueries.INSERT_QUERY
                                .Replace("{table}", table)
                                .Replace("{cols}", string.Join(",", dataToInsert.Select(m => m[0])))
                                .Replace("{values}", string.Join(",", dataToInsert.Select(m => m[1]).Select(m => string.Concat('"', m, '"'))))
            };

            var rowsAffected = command.ExecuteNonQuery();
            DestroyConnection(connection);
            return rowsAffected;
        }
    }
}
