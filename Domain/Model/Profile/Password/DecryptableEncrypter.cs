using System;
using System.Security.Cryptography;
using System.Text;

namespace ConnpassAutomator.Domain.Model.Profile.Password
{
    internal class DecryptableEncrypter
    {
        private static readonly string AES_IV = @"8tVq+b#tXUh|_euc";
        private static readonly string AES_KEY = @"BK56w#wjN-dn+zEYskDU$g.W#wd)$~Zp";
        private static readonly int KEY_SIZE = 256;
        private static readonly int BLOCK_SIZE = 128;

        internal string Encrypt(string value)
        {
            var aes = CreateAes();
            var byteValue = Encoding.UTF8.GetBytes(value);
            var byteLength = byteValue.Length;
            var encryptor = aes.CreateEncryptor();
            var encryptValue = encryptor.TransformFinalBlock(byteValue, 0, byteLength);
            var base64Value = Convert.ToBase64String(encryptValue);
            return base64Value;
        }

        internal string Decrypt(string encryptValue)
        {
            var aes = CreateAes();
            var byteValue = Convert.FromBase64String(encryptValue);
            var byteLength = byteValue.Length;
            var decryptor = aes.CreateDecryptor();
            var decryptValue = decryptor.TransformFinalBlock(byteValue, 0, byteLength);
            var stringValue = Encoding.UTF8.GetString(decryptValue);
            return stringValue;
        }

        private Aes CreateAes()
        {
            var aes = Aes.Create();
            aes.KeySize = KEY_SIZE;
            aes.BlockSize = BLOCK_SIZE;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(AES_IV);
            aes.Key = Encoding.UTF8.GetBytes(AES_KEY);
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }
    }
}
