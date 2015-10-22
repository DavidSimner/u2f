using System;
using System.IO;
using System.Linq;

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

                CounterBytes = reader.ReadBytes(4);
                CounterValue = BitConverter.ToUInt32(BitConverter.IsLittleEndian ? CounterBytes.Reverse().ToArray() : CounterBytes, 0);

                Signature = reader.ReadBytes((int)(stream.Length - stream.Position));
            }
        }

        internal readonly byte UserPresenceByte;

        internal readonly byte[] CounterBytes;
        internal readonly uint CounterValue;

        internal readonly byte[] Signature;
    }
}
