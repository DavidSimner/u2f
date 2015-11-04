using System.IO;
using Org.BouncyCastle.X509;

namespace Crypto
{
    public static class X509CertificateExtensions
    {

        public static X509Certificate Load(Stream stream)
        {
            return new X509CertificateParser().ReadCertificate(stream);
        }



        public static void ThrowIfChainNotOkay(this X509Certificate leaf, X509Certificate root)
        {
            var rootPublicKey = root.GetPublicKey();

            root.Verify(rootPublicKey);
            root.CheckValidity();

            leaf.Verify(rootPublicKey);
            leaf.CheckValidity();
        }
    }
}
