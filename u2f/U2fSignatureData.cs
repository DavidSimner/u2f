using System.IO;

namespace u2f
{
    internal class U2fSignatureData
    {
        public static explicit operator U2fSignatureData(string value)
        {
            return new U2fSignatureData(Helpers.Base64Decode(value));
        }

        private U2fSignatureData(byte[] value)
        {
            using (var stream = new MemoryStream(value, false))
            using (var reader = new BinaryReader(stream))
            {
                UserPresenceByte = reader.ReadByte();

                Counter = reader.ReadUInt32();

                Signature = reader.ReadBytes((int)(stream.Length - stream.Position));
            }
        }

        internal readonly byte UserPresenceByte;

        internal readonly uint Counter;

        internal readonly byte[] Signature;
    }
}
