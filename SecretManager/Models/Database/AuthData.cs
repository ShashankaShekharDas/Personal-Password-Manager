#pragma warning disable CS8601 // Possible null reference assignment. WILL NEVER OCCUR
using SecretManager.Interfaces;
using System.Data;
using System.Data.Common;

namespace SecretManager.Models.Database
{
    public sealed class AuthData : IDatabaseTableManager
    {
        public int? PersonId { get; set; }
        public string? Secret { get; set; }
        public string? Type { get; set; }

        public List<string[]> GetColumnValuePairs()
        {
            var result = new List<string[]>();

            if (PersonId.HasValue) result.Add(["PersonId", PersonId.ToString()]);

            if (Secret != null) result.Add([ "Secret", Secret ]);
            if (Type != null) result.Add([ "Type", Type ]);

            return result;
        }

        public static IDatabaseTableManager GetRowFromReader(DbDataReader reader)
        {
            return new AuthData()
            {
                PersonId = reader.GetInt32("PersonId"),
                Secret = reader.GetString("Secret"),
                Type = reader.GetString("Type")
            };
        }
    }
}

#pragma warning restore CS8601 // Possible null reference assignment.