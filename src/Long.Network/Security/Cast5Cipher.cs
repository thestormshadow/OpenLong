using Long.Network.Security.BouncyCastle;
using System.Text;

namespace Long.Network.Security
{
    public class Cast5Cipher : ICipher
    {
        private byte[] encIvec;
        private byte[] decIvec;
        private int encNum;
        private int decNum;

        private readonly Cast5Engine cast5Engine;

        private const string DefaultSeed = "C238xs65pjy7HU9Q";

        public Cast5Cipher(string seed = DefaultSeed)
        {
            cast5Engine = new Cast5Engine();

            encIvec = new byte[8];
            decIvec = new byte[8];
            encNum = 0;
            decNum = 0;

            GenerateKeys(new object[] { Encoding.ASCII.GetBytes(seed) });
        }

        public void Decrypt(Span<byte> src, Span<byte> dst)
        {
            int length = src.Length;
            byte c, cc;
            for (int l = length, n = decNum, inc = 0, outc = 0; l > 0; l--)
            {
                if (n == 0)
                {
                    cast5Engine.EncryptBlock(decIvec, 0, decIvec, 0);
                }
                cc = src[inc++];
                c = decIvec[n];
                decIvec[n] = cc;
                dst[outc] = (byte)((c ^ cc) & 0xff);
                outc++;
                n = (n + 1) & 0x07;
                decNum = n;
            }
        }

        public void Encrypt(Span<byte> src, Span<byte> dst)
        {
            int length = src.Length;
            byte c;
            for (int l = length, n = encNum, inc = 0, outc = 0; l > 0; l--)
            {
                if (n == 0)
                {
                    cast5Engine.EncryptBlock(encIvec, 0, encIvec, 0);
                }
                c = (byte)((src[inc++] ^ encIvec[n]) & 0xff);
                dst[outc++] = c;
                encIvec[n] = c;
                n = (n + 1) & 0x07;
                encNum = n;
            }
        }

        public void GenerateKeys(object[] seeds)
        {
            byte[] encIV = new byte[8];
            byte[] decIV = new byte[8];
            byte[] key = new byte[16];

            for (int i = 0; i < Math.Min(16, (seeds[0] as byte[]).Length); i++)
            {
                key[i] = (seeds[0] as byte[])[i];
            }

            if (seeds.Length > 2)
            {
                encIV = (byte[])seeds[1];
                decIV = (byte[])seeds[2];
            }

            cast5Engine.SetKey(key);

            encIvec = encIV;
            decIvec = decIV;
            encNum = 0;
            decNum = 0;
        }
    }
}
