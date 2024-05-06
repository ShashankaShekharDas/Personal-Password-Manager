using SecretManager.Authenticator.Authenticators;
using SecretManager.Authenticator.Cryptography;
using SecretManager.Authenticator.EncryptionDecryption;
using SecretManager.Factory;
using System.Reflection.Metadata;

namespace SecretManager.Tests.Factory
{
    public class CryptogrpaphyHelperFactoryTest
    {
        [TestCase("Base64", typeof(Base64CryptographyHelper))]
        [TestCase("PlainText", typeof(PlainTextCryptoGraphyHelper))]
        public void AssertThatCorrectTypeReturnsCorrectObject(string type, Type expectedType)
        {
            var cryptoObject = CryptographyHelperFactory.GetCryptographyHelper(type);
            Assert.That(cryptoObject.GetType(), Is.EqualTo(expectedType));
        }

        [TestCase("abc")]
        [TestCase("def")]
        public void AssertThatNonSpecifiedArgumentsReturnPlainTextType(string type)
        {
            var cryptoObject = CryptographyHelperFactory.GetCryptographyHelper(type);
            Assert.That(cryptoObject.GetType(), Is.EqualTo(typeof(PlainTextCryptoGraphyHelper)));
        }
    }
}
