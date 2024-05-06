using SecretManager.Commons;
using SecretManager.Interfaces;

namespace SecretManager.Factory
{
    public static class SecretManagerFactory
    {
        public static IEnumerable<ISecretManager?> GetSecretManagerInstance(AuthenticatorTypes type, out ISecretManager? activeSecretManager)
        {
            //Later will be replaced with features
            activeSecretManager = null;
            string sectionName = $"secretManagerActive/{type}";
            try
            {
                var configurations = AppConfigReader.ReadKeyValuePair(sectionName).Where(n => n.Value is "Active" or "Enabled");
                var activeManager = configurations.First(n => n.Value == "Active").Key;

#pragma warning disable CS8604 // Possible null reference argument.

                activeSecretManager = (ISecretManager?)Activator.CreateInstance(Type.GetType(activeManager));
                return configurations.Select(n => (ISecretManager?)Activator.CreateInstance(Type.GetType(n.Key)));

#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch
            {
                return [];
            }
        }
    }
}
