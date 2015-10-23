using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;

namespace Crypto
{
    public static class EllipticCurve
    {
        private static readonly DerObjectIdentifier _oid = SecObjectIdentifiers.SecP256r1;

        public static AsymmetricKeyParameter LoadPublicKey(byte[] value)
        {
            return new ECPublicKeyParameters("EC", SecNamedCurves.GetByOid(_oid).Curve.DecodePoint(value), _oid);
        }
    }
}
