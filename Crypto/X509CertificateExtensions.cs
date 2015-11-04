using Org.BouncyCastle.X509;

namespace Crypto
{
    public static class X509CertificateExtensions
    {




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
