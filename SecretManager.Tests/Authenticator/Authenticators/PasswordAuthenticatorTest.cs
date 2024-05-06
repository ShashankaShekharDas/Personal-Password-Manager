using SecretManager.Authenticator.Authenticators;

namespace SecretManager.Tests.Authenticator.Authenticators
{
    public class PasswordAuthenticatorTest
    {
        private PasswordAuthenticator _passwordAuthenticator;
        private Mock<ISecretManager> _secretManagerMock;

        [OneTimeSetUp]
        public void SetUp()
        {
            _secretManagerMock = new();
            _secretManagerMock.Setup(m => m.Authenticate(It.Is<string>(password => password == "abc"))).Returns(true);
            _passwordAuthenticator = new PasswordAuthenticator(_secretManagerMock.Object);
        }

        [TestCase("abc", true)]
        [TestCase("def", false)]
        public void AssertThatPasswordAuthenticatesCorrectly(string password, bool expected)
        {
            var result = _passwordAuthenticator.Authenticate(password);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
