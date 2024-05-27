using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NoteTakingApp.Services
{
    public class StringToIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string number = reader.GetString();
            if (int.TryParse(number, out int result))
            {
                return result;
            }
            return 0; // or throw an exception
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
