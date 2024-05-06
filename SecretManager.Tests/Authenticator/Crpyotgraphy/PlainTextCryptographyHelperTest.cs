using SecretManager.Authenticator.EncryptionDecryption;

namespace SecretManager.Tests.Authenticator.Crpyotgraphy
{
    public class PlainTextCryptographyHelperTest
    {
        private PlainTextCryptoGraphyHelper _plainTextCryptographyHelper;

        [OneTimeSetUp]
        public void SetUp()
        {
            _plainTextCryptographyHelper = new();
        }

        [TestCase("thisIsAVerySecurePassword", "thisIsAVerySecurePassword")]
        [TestCase("password123", "password123")]
        public void AssertThatSecretsAreEncodedToBase64Correctly(string secret, string expected)
        {
            var encoded = _plainTextCryptographyHelper.Encipher(secret);
            Assert.That(encoded, Is.EqualTo(expected));
        }

        [TestCase("thisIsAVerySecurePassword", "thisIsAVerySecurePassword")]
        [TestCase("password123", "password123")]
        public void AssertThatSecretsAreDecodedFromBase64Correctly(string encoded, string actual)
        {
            var decoded = _plainTextCryptographyHelper.Decipher(encoded);
            Assert.That(decoded, Is.EqualTo(actual));
        }
    }
}
