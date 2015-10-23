using System.IO;

namespace u2f
{
    internal class U2fRegistrationData
    {
        public static explicit operator U2fRegistrationData(string value)
        {
            return new U2fRegistrationData(Helpers.Base64Decode(value));
        }

        private U2fRegistrationData(byte[] value)
        {
            using (var stream = new MemoryStream(value, false))
            using (var reader = new BinaryReader(stream))
            {
                ReservedByte = reader.ReadByte();

                UserPublicKey = reader.ReadBytes(65);

                KeyHandle = reader.ReadBytes(reader.ReadByte());

                AttestationCertificateAndSignature = reader.ReadBytes((int)(stream.Length - stream.Position));
            }
        }

        internal readonly byte ReservedByte;

        internal readonly byte[] UserPublicKey;

        internal readonly byte[] KeyHandle;

        internal readonly byte[] AttestationCertificateAndSignature;
    }
}
