namespace SecretManager.Commons
{
    public static class SqlQueries
    {
        public static readonly string INSERT_QUERY = "INSERT INTO {table}({cols}) VALUES ({values})";
        public static readonly string SELECT_QUERY = "SELECT * FROM {table} WHERE {conditions}";
    }
}
