using SecretManager.Authenticator.Models;

namespace SecretManager.Authenticator.SecretManagers
{
    internal sealed class FileSecretManager(ICryptographyHelper encryptionDecryptionHelper) : ISecretManager
    {
        private readonly string _secretStorageLocation = "C:\\Users\\gudur\\OneDrive\\Desktop\\Learn\\C#\\SecretManager\\SecretManager\\TestSecretStore\\";
        private readonly string _secretStorageFile = "credentials.txt";
        private readonly ICryptographyHelper _encryptionDecryptionHelper = encryptionDecryptionHelper;

        public bool Authenticate(string secret)
        {
            var readSecret = File.ReadAllText(Path.Combine(_secretStorageLocation, _secretStorageFile))
                .Replace(Environment.NewLine, string.Empty);
            return !string.IsNullOrEmpty(secret) && 
                _encryptionDecryptionHelper.Decipher(readSecret) == secret;
        }

        public bool Store(string secret)
        {
            try
            {
                using StreamWriter outputFile = new(path: Path.Combine(_secretStorageLocation, _secretStorageFile), append: false);
                outputFile.WriteLine(_encryptionDecryptionHelper.Encipher(secret));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
