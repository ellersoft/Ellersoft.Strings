using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evbpc.Strings
{
    public class ValidatedStringJsonNetConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteValue((value as ValidatedString).String);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
            Activator.CreateInstance(objectType, reader.Value);

        public override bool CanConvert(Type objectType)
        {
#if NETSTANDARD_1_0
            try
            {
                return Activator.CreateInstance(objectType) is ValidatedString;
            }
            catch
            {
                // If we can't make an instance it's definitely not our type
                return false;
            }
#else
            return objectType.IsSubclassOf(typeof(ValidatedString)) || objectType == typeof(ValidatedString);
#endif
        }
    }
}
