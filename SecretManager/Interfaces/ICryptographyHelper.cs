namespace SecretManager.Interfaces
{
    public interface ICryptographyHelper
    {
        string Encipher(string data);
        string Decipher(string data);
    }
}
