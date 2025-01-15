using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormat;

    public JsonDateTimeConverter(string dateFormat)
    {
        _dateFormat = dateFormat;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), _dateFormat, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormat));
    }
}
