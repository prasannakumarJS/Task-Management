namespace Api.Security
{
    public interface ICryptographyHelper
    {
        byte[] Decrypt(byte[] buffer);

        string Decrypt(string encrypted);

        string DecryptFromUrl(string encrypted);

        byte[] Encrypt(byte[] buffer);

        string Encrypt(string unencrypted);

        string EncryptToUrl(string unencrypted);
    }
}
