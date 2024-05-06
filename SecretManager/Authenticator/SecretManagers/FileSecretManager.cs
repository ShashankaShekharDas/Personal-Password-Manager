﻿using SecretManager.Interfaces;

namespace SecretManager.Authenticator.SecretManagers
{
    public sealed class FileSecretManager(ICryptographyHelper encryptionDecryptionHelper) : ISecretManager
    {
        private readonly string _secretStorageLocation = "C:\\Users\\gudur\\OneDrive\\Desktop\\Learn\\C#\\SecretManager\\SecretManager\\TestSecretStore\\";
        private readonly string _secretStorageFile = "credentials.txt";
        private readonly ICryptographyHelper _encryptionDecryptionHelper = encryptionDecryptionHelper;

        public FileSecretManager(ICryptographyHelper encryptionDecryptionHelper, string secretStorageLocation, string secretStorageFile) : this(encryptionDecryptionHelper)
        {
            _secretStorageLocation = secretStorageLocation;
            _secretStorageFile = secretStorageFile;
        }

        public bool Authenticate(string secret)
        {
            var path = Path.Combine(_secretStorageLocation, _secretStorageFile);
            if (!File.Exists(path))
            {
                return false;
            }
            var readSecret = File.ReadAllText(path)
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
