using SecretManager.Authenticator.Models;

namespace SecretManager.Authenticator.EncryptionDecryption
{
    internal sealed class PlainTextCryptoGraphyHelper : ICryptographyHelper
    {
        public string Decipher(string data) => data;

        public string Encipher(string data) => data;
    }
}
