using Newtonsoft.Json;

namespace u2f.DTOs
{
    internal class U2fSignResponseData
    {
        [JsonConverter(typeof(WebSafeBinaryConverter))]
        internal byte[] KeyHandle { get; private set; }

        internal U2fClientData ClientData { get; private set; }

        internal U2fSignatureData SignatureData { get; private set; }
    }
}
