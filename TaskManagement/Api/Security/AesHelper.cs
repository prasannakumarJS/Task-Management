using System.Security.Cryptography;

namespace Api.Security
{
    public class AesHelper : CryptographyHelper
    {
        public AesHelper(string keyStr, string vectorStr)
            : base(() => Aes.Create(), keyStr, vectorStr)
        {
        }
    }
}