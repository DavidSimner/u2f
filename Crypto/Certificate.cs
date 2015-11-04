using System.IO;
using Org.BouncyCastle.X509;

namespace Crypto
{
    public class Certificate
    {
        private readonly X509Certificate _certificate;

        public static Certificate Load(Stream stream)
        {
            return new Certificate(new X509CertificateParser().ReadCertificate(stream));
        }

        private Certificate(X509Certificate _certificate)
        {
            this._certificate = _certificate;
        }

        public PublicKey PublicKey
        {
            get
            {
                return new PublicKey(_certificate.GetPublicKey());
            }
        }

        public void ThrowIfChainNotOkay(X509Certificate root)
        {
            var rootPublicKey = root.GetPublicKey();

            root.Verify(rootPublicKey);
            root.CheckValidity();

            _certificate.Verify(rootPublicKey);
            _certificate.CheckValidity();
        }
    }
}
