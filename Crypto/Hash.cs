using Org.BouncyCastle.Crypto.Digests;
using System.Text;

namespace Crypto
{
    public static class Hash
    {
        public static byte[] String(string value)
        {
            return Array(Encoding.ASCII.GetBytes(value));
        }

        public static byte[] Array(byte[] value)
        {
            var digest = new Sha256Digest();
            digest.BlockUpdate(value, 0, value.Length);

            var ret = new byte[digest.GetDigestSize()];
            digest.DoFinal(ret, 0);
            return ret;
        }
    }
}
