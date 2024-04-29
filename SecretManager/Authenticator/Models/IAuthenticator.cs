namespace SecretManager.Authenticator.Models
{
    public interface IAuthenticator
    {
        bool Authenticate(string password);
        AuthenticatorTypes GetType();
    }

    public enum AuthenticatorTypes
    {
        Fingerprint,
        Password
    }
}
