using System.Security.Cryptography;
using System.Text;

namespace Long.Network.Security
{
    public sealed class AesCipher : ICipher
    {
        public readonly static string SharedKey = "8y/B?E(H+MbQeThWmZq3t6w9z$C&F)J@";
        public readonly static string SharedIv = "t6w9z$C&F)J@NcRf";

        private Aes aes;
        private ICryptoTransform mEncryptor;
        private ICryptoTransform mDecryptor;

        private byte[] mEncryptIv;
        private byte[] mDecryptIv;

        private AesCipher()
        {
        }

        [Obsolete("Do not use this method with shared keys or change the Shared Keys to private ones.", false)]
        public static AesCipher Create()
        {
            return Create(SharedKey, SharedIv, SharedIv);
        }

        public static AesCipher Create(string key, string eIv, string dIv)
        {
            var cipher = new AesCipher
            {
                aes = Aes.Create()
            };

            cipher.aes.Mode = CipherMode.CFB;
            cipher.aes.KeySize = 256;
            cipher.aes.BlockSize = 128;
            cipher.aes.FeedbackSize = 8;
            cipher.aes.Padding = PaddingMode.None;

            cipher.aes.Key = Convert.FromBase64String(key);
            cipher.mEncryptIv = Convert.FromBase64String(eIv);
            cipher.mDecryptIv = Convert.FromBase64String(dIv);

            cipher.mEncryptor = cipher.aes.CreateEncryptor(cipher.aes.Key, cipher.mEncryptIv);
            cipher.mDecryptor = cipher.aes.CreateDecryptor(cipher.aes.Key, cipher.mDecryptIv);
            return cipher;
        }

        /// <inheritdoc />
        public void GenerateKeys(params object[] seeds)
        {
            byte[] key = (byte[])seeds[0];
            key.CopyTo(new Memory<byte>(aes.Key));

            if (seeds.Length > 2)
            {
                byte[] eIv = (byte[])seeds[1];
                byte[] dIv = (byte[])seeds[2];
                eIv.CopyTo(new Memory<byte>(mEncryptIv));
                dIv.CopyTo(new Memory<byte>(mDecryptIv));
            }
            else
            {
                Console.WriteLine($"Using shared encryption settings!!! Not recommended on production environment");

                byte[] iv = Encoding.ASCII.GetBytes(SharedIv);

                mEncryptIv = new byte[iv.Length];
                mDecryptIv = new byte[iv.Length];

                iv.CopyTo(mEncryptIv, 0);
                iv.CopyTo(mDecryptIv, 0);
            }

            mEncryptor = aes.CreateEncryptor(aes.Key, mEncryptIv);
            mDecryptor = aes.CreateDecryptor(aes.Key, mDecryptIv);
        }

        /// <inheritdoc />
        public void Decrypt(Span<byte> src, Span<byte> dst)
        {
            using MemoryStream memory = new();
            using CryptoStream stream = new(memory, mDecryptor, CryptoStreamMode.Write);
            stream.Write(src);
            stream.FlushFinalBlock();

            byte[] decrypted = memory.ToArray();
            for (int i = 0; i < decrypted.Length; i++)
            {
                dst[i] = decrypted[i];
            }
        }

        /// <inheritdoc />
        public void Encrypt(Span<byte> src, Span<byte> dst)
        {
            using var memory = new MemoryStream();
            using var stream = new CryptoStream(memory, mEncryptor, CryptoStreamMode.Write);
            stream.Write(src);
            stream.FlushFinalBlock();

            byte[] encrypted = memory.ToArray();
            for (int i = 0; i < encrypted.Length; i++)
            {
                dst[i] = encrypted[i];
            }
        }

        public class Settings
        {
            public string Key { get; set; }
            public string EncryptIV { get; set; }
            public string DecryptIV { get; set; }
        }
    }
}
