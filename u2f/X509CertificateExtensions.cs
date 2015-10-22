using Org.BouncyCastle.X509;

namespace u2f
{
    internal static class X509CertificateExtensions
    {
        internal static void ThrowIfChainNotOkay(this X509Certificate leaf, X509Certificate root)
        {
            var rootPublicKey = root.GetPublicKey();

            root.Verify(rootPublicKey);
            root.CheckValidity();

            leaf.Verify(rootPublicKey);
            leaf.CheckValidity();
        }
    }
}
