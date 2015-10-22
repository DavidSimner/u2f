using Org.BouncyCastle.X509;
using System.IO;

namespace u2f
{
    internal class U2fRegistrationData
    {
        public static explicit operator U2fRegistrationData(string value)
        {
            return new U2fRegistrationData(value);
        }

        private U2fRegistrationData(string value)
        {
            using (var stream = new MemoryStream(Helpers.Base64Decode(value), false))
            using (var reader = new BinaryReader(stream))
            {
                ReservedByte = reader.ReadByte();

                UserPublicKey = reader.ReadBytes(65);

                KeyHandle = reader.ReadBytes(reader.ReadByte());

                AttestationCertificate = new X509CertificateParser().ReadCertificate(stream);

                Signature = reader.ReadBytes((int)(stream.Length - stream.Position));
            }
        }

        internal readonly byte ReservedByte;

        internal readonly byte[] UserPublicKey;

        internal readonly byte[] KeyHandle;

        internal readonly X509Certificate AttestationCertificate;

        internal readonly byte[] Signature;
    }
}
