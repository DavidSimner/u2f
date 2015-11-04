using Newtonsoft.Json;
using System;

namespace u2f.Converters
{
    internal class WebSafeBinaryConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Helpers.Base64Decode(serializer.Deserialize<string>(reader));
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotSupportedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}
