
using System;
using System.Security.Cryptography;
using System.Text;
using TestAngular.Infrastructure;

namespace TestAngular.Applications
{
    public class CryptoApplication : ICryptoApplication
    {
        private const int AesKeySizeBits = 256;
        private const int AesBlockSizeBits = 128;
        private readonly byte[] AesKey;
        private readonly byte[] AesIv;

        public CryptoApplication()
        {
            var aesKeyDerivationSalt = new byte[] { 9, 2, 5, 3, 7, 1, 4, 6 };
            const int aesKeyDerivationIterations = 1000;
            const string aesPassphrase = "hZGSDhioq23z598y2398qiuPXKFer435sdrgs";

            using (var hashProvider = new SHA256CryptoServiceProvider())
            {
                var passphraseBytes = Encoding.UTF8.GetBytes(aesPassphrase);
                var hash = hashProvider.ComputeHash(passphraseBytes);
                using (var key = new Rfc2898DeriveBytes(hash, aesKeyDerivationSalt, aesKeyDerivationIterations))
                {
                    AesKey = key.GetBytes(AesKeySizeBits / 8);
                    AesIv = key.GetBytes(AesBlockSizeBits / 8);
                }
            }
        }

        public string DeryptText(string inputText)
        {
            using (RijndaelManaged aes = GetAesCipher())
            using (var decryptor = aes.CreateDecryptor())
            {
                byte[] dataToDecrypt = Convert.FromBase64String(inputText ?? "");
                byte[] result = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                return Encoding.UTF8.GetString(result);
            }
        }

        private RijndaelManaged GetAesCipher()
        {
            return new RijndaelManaged()
            {
                KeySize = AesKeySizeBits,
                BlockSize = AesBlockSizeBits,
                Key = AesKey,
                IV = AesIv,
                Mode = CipherMode.CBC
            };
        }
    }
}
