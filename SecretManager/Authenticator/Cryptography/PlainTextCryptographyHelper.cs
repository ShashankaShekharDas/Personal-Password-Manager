using SecretManager.Interfaces;

namespace SecretManager.Authenticator.EncryptionDecryption
{
    public sealed class PlainTextCryptoGraphyHelper : ICryptographyHelper
    {
        public string Decipher(string data) => data;

        public string Encipher(string data) => data;
    }
}
