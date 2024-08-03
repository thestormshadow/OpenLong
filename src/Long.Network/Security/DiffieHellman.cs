using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Serilog;

namespace Long.Network.Security
{
    public sealed class DiffieHellman
    {
        private static readonly ILogger logger = Log.ForContext<DiffieHellman>();

        private const string P = "92005BF0B79625488ADC282C144037A977E4BCB81D2D08452B57000178CD3AC9";
        private const string G = "F0";

        private DHAgreement mAgreement;
        private DHPublicKeyParameters mPublic;
        private DHPrivateKeyParameters mPrivate;

        private DiffieHellman()
        {
            PrimeRoot = new BigInteger(P, 16);
            Generator = new BigInteger(G, 16);
        }

        public static DiffieHellman Create()
        {
            try
            {
                var diffieHellman = new DiffieHellman();
                DHKeyPairGenerator gen = diffieHellman.GetKeyPairGenerator();
                AsymmetricCipherKeyPair pair = gen.GenerateKeyPair();

                diffieHellman.mPublic = (DHPublicKeyParameters)pair.Public;
                diffieHellman.mPrivate = (DHPrivateKeyParameters)pair.Private;
                diffieHellman.mAgreement = new DHAgreement();
                diffieHellman.mAgreement.Init(new ParametersWithRandom(diffieHellman.mPrivate, new SecureRandom()));
                diffieHellman.Modulus = diffieHellman.mAgreement.CalculateMessage();

                diffieHellman.PrivateKey = diffieHellman.mPrivate.X;
                diffieHellman.PublicKey = diffieHellman.mPublic.Y;

                return diffieHellman;
            }
#if DEBUG
            catch (Exception ex)
            {
                logger.Fatal(ex, "Error creating new DiffieHellman instance");
                return null;
            }
#else
            catch
            {
                return null;
            }
#endif
        }

        public bool Initialize(byte[] publicKey, byte[] modulus)
        {
            try
            {
                // 1.2.840.113549.1.3.1
                var targetKey = new DHPublicKeyParameters(
                    new BigInteger(1, publicKey),
                    new DHParameters(PrimeRoot, Generator),
                    new DerObjectIdentifier("1.2.840.113549.1.3.1")
                );

                SharedKey = mAgreement.CalculateAgreement(targetKey, new BigInteger(1, modulus));
                return true;
            }
#if DEBUG
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return false;
            }
#else
            catch
            {
                return false;
            }
#endif
        }

        public BigInteger PrimeRoot { get; set; }
        public BigInteger Generator { get; set; }
        public BigInteger Modulus { get; set; }
        public BigInteger PublicKey { get; private set; }
        public BigInteger PrivateKey { get; private set; }
        public BigInteger SharedKey { get; private set; }

        private DHKeyPairGenerator GetKeyPairGenerator()
        {
            var kpGen = new DHKeyPairGenerator();
            kpGen.Init(new DHKeyGenerationParameters(new SecureRandom(), new DHParameters(PrimeRoot, Generator)));
            return kpGen;
        }
    }
}
