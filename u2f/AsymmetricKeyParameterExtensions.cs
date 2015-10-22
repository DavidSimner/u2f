using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Signers;
using System;

namespace u2f
{
    internal static class AsymmetricKeyParameterExtensions
    {
        internal static void ThrowIfSignatureNotOkay(this AsymmetricKeyParameter publicKey, byte[] signature, params byte[][] inputs)
        {
            var signer = new DsaDigestSigner(new ECDsaSigner(), new Sha256Digest());
            signer.Init(false, publicKey);
            
            foreach (var input in inputs)
            {
                signer.BlockUpdate(input, 0, input.Length);
            }

            if (!signer.VerifySignature(signature))
            {
                throw new ApplicationException();
            }
        }
    }
}
