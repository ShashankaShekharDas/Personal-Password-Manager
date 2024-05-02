using System.Data.Common;

namespace SecretManager.Interfaces
{
    public interface IDatabaseTableManager
    {
        public List<string[]> GetColumnValuePairs();

        public static abstract IDatabaseTableManager GetRowFromReader(DbDataReader reader);
    }
}
