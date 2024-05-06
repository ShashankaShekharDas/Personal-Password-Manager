using SecretManager.Authenticator.Cryptography;

namespace SecretManager.Tests.Authenticator.Crpyotgraphy
{
    public class Base64CryptographyHelperTest
    {
        private Base64CryptographyHelper _base64CryptographyHelper;

        [OneTimeSetUp]
        public void SetUp()
        {
            _base64CryptographyHelper = new();
        }

        [TestCase("thisIsAVerySecurePassword", "dGhpc0lzQVZlcnlTZWN1cmVQYXNzd29yZA==")]
        [TestCase("password123", "cGFzc3dvcmQxMjM=")]
        public void AssertThatSecretsAreEncodedToBase64Correctly(string secret, string expected)
        {
            var encoded = _base64CryptographyHelper.Encipher(secret);
            Assert.That(encoded, Is.EqualTo(expected));
        }

        [TestCase("dGhpc0lzQVZlcnlTZWN1cmVQYXNzd29yZA==", "thisIsAVerySecurePassword")]
        [TestCase("cGFzc3dvcmQxMjM=", "password123")]
        public void AssertThatSecretsAreDecodedFromBase64Correctly(string encoded, string actual)
        {
            var decoded = _base64CryptographyHelper.Decipher(encoded);
            Assert.That(decoded, Is.EqualTo(actual));
        }
    }
}
