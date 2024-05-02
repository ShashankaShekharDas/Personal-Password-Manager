using System.Collections;
using System.Configuration;

namespace SecretManager.Commons
{
    public static class AppConfigReader
    {
        public static Dictionary<string, string?> ReadKeyValuePair(string sectionName)
        {
            return (ConfigurationManager.GetSection(sectionName) as Hashtable ?? [])
                    .Cast<DictionaryEntry>()
                    .ToDictionary(n => n.Key.ToString() ?? "", n => n.Value is not null ? n.Value.ToString() : "");
        }
    }
}
