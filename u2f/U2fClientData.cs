using Newtonsoft.Json;
using System.Text;

namespace u2f
{
    internal class U2fClientData
    {
        public static explicit operator U2fClientData(string value)
        {
            return new U2fClientData(value);
        }

        private U2fClientData(string value)
        {
            Raw = value;
            JsonConvert.PopulateObject(Encoding.ASCII.GetString(Helpers.Base64Decode(value)), this, Helpers.JsonSerializerSettings);
        }

        internal readonly string Raw;

        internal string Typ { get; private set; }

        internal string Challenge { get; private set; }

        internal string Origin { get; private set; }

        internal string CId_PubKey { get; private set; }
    }
}
