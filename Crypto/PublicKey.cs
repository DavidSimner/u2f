using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Signers;
using System;

namespace Crypto
{
    public class PublicKey
    {
        private readonly AsymmetricKeyParameter _publicKey;

        public PublicKey(AsymmetricKeyParameter publicKey)
        {
            _publicKey = publicKey;
        }

        public void ThrowIfSignatureNotOkay(byte[] signature, params byte[][] inputs)
        {
            var signer = new DsaDigestSigner(new ECDsaSigner(), new Sha256Digest());
            signer.Init(false, _publicKey);
            
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
