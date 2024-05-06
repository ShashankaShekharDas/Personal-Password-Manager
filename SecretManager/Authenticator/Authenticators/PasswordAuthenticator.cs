using SecretManager.Factory;
using SecretManager.Interfaces;

namespace SecretManager.Authenticator.Authenticators
{
    public sealed class PasswordAuthenticator : IAuthenticator
    {
        private readonly ISecretManager? _activeSecretManager;

        public PasswordAuthenticator()
        {
            SecretManagerFactory.GetSecretManagerInstance(GetType(), out _activeSecretManager);
        }

        public PasswordAuthenticator(ISecretManager? activeSecretManager)
        {
            _activeSecretManager = activeSecretManager;
        }

        public bool Authenticate(string password)
        {
            return _activeSecretManager is not null && _activeSecretManager.Authenticate(password);
        }

        public new AuthenticatorTypes GetType()
        {
            return AuthenticatorTypes.Password;
        }
    }
}
