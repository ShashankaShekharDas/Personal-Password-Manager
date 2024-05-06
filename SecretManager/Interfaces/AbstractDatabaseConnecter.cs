using SecretManager.Models.Database;
using System.Data.Common;

namespace SecretManager.Interfaces
{
    //Does it need to be abstract or Can it be an interface?
    public abstract class AbstractDatabaseConnecter<T>(DatabaseConnectionRecord record, string database) where T : IDatabaseTableManager
    {
        protected readonly DatabaseConnectionRecord _record = record;
        protected readonly string _database = database;

        protected abstract DbConnection CreateConnection();

        protected virtual void DestroyConnection(DbConnection connection) => connection.Dispose();

        public abstract int InsertData(string table, T tableData); // Make the columns & values together as 1 Record

        public abstract IEnumerable<T> GetData(string table, T tableData);
    }
}
