using System.IO;
using System.Text;
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

        internal static Certificate Load(string value)
        {
            return new Certificate(new X509CertificateParser().ReadCertificate(Encoding.ASCII.GetBytes(value)));
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

        public void ThrowIfChainNotOkay(Certificate root)
        {
            var rootPublicKey = root._certificate.GetPublicKey();

            root._certificate.Verify(rootPublicKey);
            root._certificate.CheckValidity();

            _certificate.Verify(rootPublicKey);
            _certificate.CheckValidity();
        }
    }
}
