using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonStorageExamples
{
    class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime readInValue = DateTime.ParseExact(reader.GetString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return readInValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
        }
    }
}
