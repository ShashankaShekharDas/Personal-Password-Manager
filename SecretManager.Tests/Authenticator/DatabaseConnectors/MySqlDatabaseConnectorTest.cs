using MySql.Data.MySqlClient;
using PeanutButter.TempDb.MySql.Data;
using SecretManager.Authenticator.DatabaseConnectors;
using SecretManager.Models.Database;
using System.Data.Common;

namespace SecretManager.Tests.Authenticator.DatabaseConnectors
{
    public class MySqlDatabaseConnectorTest
    {
        private AuthData _insertAuthRecord;
        private MySqlDatabaseConnectorMock<AuthData> _mySqlMock;
        private readonly string _tableName = "AUTH";

        [SetUp]
        public void SetUp()
        {
            _mySqlMock = new("", "", "", "");
            _insertAuthRecord = new()
            {
                PersonId = 100,
                Secret = "secret",
                Type = "password"
            };
        }

        [TestCase(1, 1)]
        [TestCase(2, 0)]
        public void AssertThatGetDataQueryReturnsExpectedResult(int personId, int expectedNumberOfResults)
        {
            var result = _mySqlMock.GetData(_tableName, new AuthData() { PersonId = personId });
            Assert.That(result.Count, Is.EqualTo(expectedNumberOfResults));
        }

        [Test]
        public void AssertThatInsertDataQueryReturnsCorrectNumberOfRows()
        {
            var rowsAffected = _mySqlMock.InsertData(_tableName, _insertAuthRecord);
            Assert.That(rowsAffected, Is.EqualTo(1));
        }
    }

    public class MySqlDatabaseConnectorMock<T> : MySqlDatabaseConnector<T>
        where T : IDatabaseTableManager
    {

        private readonly TempDBMySql _localDb;
        private readonly DbConnection _connection;

        public MySqlDatabaseConnectorMock(string server, string username, string password, string database) : base(server, username, password, database)
        {
            _localDb = new();
            _connection = _localDb.OpenConnection();
        }

        protected override DbConnection CreateConnection()
        {
            InitializeTables();
            return _connection;
        }

        private void InitializeTables()
        {
            var command = new MySqlCommand
            {
                Connection = (MySqlConnection)_connection,
                CommandText = "CREATE TABLE AUTH (PersonId int,Secret varchar(255), Type varchar(255));"
            };
            command.ExecuteNonQuery();

            command = new MySqlCommand
            {
                Connection = (MySqlConnection)_connection,
                CommandText = "INSERT INTO AUTH(PersonId, Secret, Type) VALUES (\"1\", \"def\", \"TEMP2\")"
            };
            command.ExecuteNonQuery();
        }
    }
}
