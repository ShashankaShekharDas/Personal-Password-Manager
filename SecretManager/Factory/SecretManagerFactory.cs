using System.Collections;
using System.Configuration;
using SecretManager.Authenticator.Models;

namespace SecretManager.Factory
{
    public class SecretManagerFactory
    {
        private static string _assemblyName;

        static SecretManagerFactory() => _assemblyName = typeof(Program).Assembly.GetName().Name ?? "";

        public static void SetAssembly(string assemblyName) => _assemblyName = assemblyName;

        public static IEnumerable<ISecretManager?> GetSecretManagerInstance(AuthenticatorTypes type, out ISecretManager? activeSecretManager)
        {
            //Later will be replaced with features
            activeSecretManager = null; 
            string sectionName = $"secretManagerActive/{type}";
            try
            {
                var configurations = (ConfigurationManager.GetSection(sectionName) as Hashtable ?? [])
                    .Cast<DictionaryEntry>()
                    .ToDictionary(n => n.Key.ToString() ?? "", n => n.Value is not null ? n.Value : "")
                    .Where(n => (string)n.Value is "Active" or "Enabled");
                var activeManager = configurations.Where(n => (string)n.Value == "Active").First().Key;

#pragma warning disable CS8604 // Possible null reference argument.
                activeSecretManager = (ISecretManager?)Activator.CreateInstance(Type.GetType(activeManager));
                return configurations.Select(n => (ISecretManager?)Activator.CreateInstance(Type.GetType(n.Key)));
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return [];
            }
        }
    }
}
