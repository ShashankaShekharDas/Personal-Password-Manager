namespace SecretManager.Interfaces
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
