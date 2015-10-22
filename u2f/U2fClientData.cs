using Newtonsoft.Json;
using System.Text;

namespace u2f
{
    internal class U2fClientData
    {
        public static explicit operator U2fClientData(string value)
        {
            return new U2fClientData(Helpers.Base64Decode(value));
        }

        private U2fClientData(byte[] value)
        {
            Raw = value;
            JsonConvert.PopulateObject(Encoding.ASCII.GetString(value), this, Helpers.JsonSerializerSettings);
        }

        internal readonly byte[] Raw;

        internal string Typ { get; private set; }

        internal string Challenge { get; private set; }

        internal string Origin { get; private set; }

        internal string CId_PubKey { get; private set; }
    }
}
