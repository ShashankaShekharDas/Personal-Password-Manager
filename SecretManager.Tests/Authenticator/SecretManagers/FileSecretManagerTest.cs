using SecretManager.Authenticator.Cryptography;
using SecretManager.Authenticator.SecretManagers;

namespace SecretManager.Tests.Authenticator.SecretManagers
{
    public class FileSecretManagerTest
    {
        private FileSecretManager _fileSecretManager;
        private ICryptographyHelper _cryptoHelper;
        private readonly string _directory = Directory.GetCurrentDirectory();
        private readonly string _file = "testFile.txt";

        [SetUp]
        public void SetUp()
        {
            _cryptoHelper = new Base64CryptographyHelper();
            _fileSecretManager = new FileSecretManager(_cryptoHelper, _directory, _file);
        }

        [Test]
        public void AssertThatWritingSecretsToFileIsSuccesfulAndCorrectEncryptedSecretsAreStored()
        {
            var success = _fileSecretManager.Store("abc");
            var secret = _cryptoHelper.Decipher(File.ReadAllText(Path.Combine(_directory, _file)).Replace(Environment.NewLine, string.Empty));

            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(secret, Is.EqualTo("abc"));
            });
        }

        [Test]
        public void AssertThatIfSecretIsNotPresentAuthenticationFails()
        {
            var secretManager = new FileSecretManager(_cryptoHelper, _directory, _file + Guid.NewGuid());
            Assert.That(secretManager.Authenticate("xyz"), Is.False);
        }

        [Test]
        public void AssertThatIfSecretIsNotMatchAuthenticationFails()
        {
            _fileSecretManager.Store("abc");
            var authenticated = _fileSecretManager.Authenticate("def");
            Assert.That(authenticated, Is.False);
        }
    }
}
