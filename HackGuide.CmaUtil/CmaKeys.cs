using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HackGuide.CmaUtil
{
    public class CmaKeys
    {
        //Taken from Chovy-Sign https://github.com/KuromeSan/chovy-sign
        //https://github.com/KuromeSan/chovy-sign/blob/master/CHOVY-SIGN/cmakeys.cs
        static byte[] Passphrase = Encoding.ASCII.GetBytes("Sri Jayewardenepura Kotte");
        static byte[] Key = { 0xA9, 0xFA, 0x5A, 0x62, 0x79, 0x9F, 0xCC, 0x4C, 0x72, 0x6B, 0x4E, 0x2C, 0xE3, 0x50, 0x6D, 0x38 };

        public string GenerateKeyStr(string aid)
        {
            try
            {
                long asLong = Convert.ToInt64(aid, 16);

                byte[] aidBytes = BitConverter.GetBytes(asLong);
                Array.Reverse(aidBytes);

                byte[] keyBytes = this.GenerateKey(aidBytes);

                return BitConverter.ToString(keyBytes).Replace("-", "");
            }
            catch (Exception)
            {
                return "INVALID_AID";
            }
        }
        public byte[] GenerateKey(byte[] aid)
        {
            var ms = new MemoryStream();
            ms.Write(aid, 0, aid.Length);
            ms.Write(Passphrase, 0, Passphrase.Length);
            byte[] keyBytes = ms.ToArray();
            ms.Dispose();

            SHA256 sha = SHA256.Create();
            keyBytes = sha.ComputeHash(keyBytes);
            sha.Dispose();

            keyBytes = decrypt(keyBytes, Key);

            return keyBytes;
        }

        private static byte[] decrypt(byte[] cipherData, byte[] Key)
        {
            MemoryStream ms = new MemoryStream();
            Aes alg = Aes.Create();
            alg.Mode = CipherMode.CBC;
            alg.Padding = PaddingMode.None;
            alg.KeySize = 128;
            alg.Key = Key;
            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }
    }
}
