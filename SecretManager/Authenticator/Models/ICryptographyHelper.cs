namespace SecretManager.Authenticator.Models
{
    public interface ICryptographyHelper
    {
        string Encipher(string data);
        string Decipher(string data);
    }
}
