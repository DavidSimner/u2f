using Newtonsoft.Json;

namespace u2f
{
    internal class U2fSignResponseData
    {
        [JsonConverter(typeof(WebSafeBinaryConverter))]
        internal byte[] KeyHandle { get; private set; }

        internal U2fClientData ClientData { get; private set; }

        internal string SignatureData { get; private set; }
    }
}
