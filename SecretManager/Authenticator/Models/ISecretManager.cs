namespace SecretManager.Authenticator.Models
{
    public interface ISecretManager
    {
        // Get Instance of ISecretManager based on authenticator type
        // Factory will create the object
        bool Store(string secret);
        bool Authenticate(string secret);
    }
}
