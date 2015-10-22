using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace u2f
{
    internal static class Helpers
    {
        internal static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { DefaultMembersSearchFlags = BindingFlags.NonPublic | BindingFlags.Instance },
                MissingMemberHandling = MissingMemberHandling.Error
            };

        internal static byte[] Base64Decode(string value)
        {
            return Convert.FromBase64String(value.Replace('-', '+').Replace('_', '/') + new string('=', 4 - value.Length % 4));
        }
    }
}
