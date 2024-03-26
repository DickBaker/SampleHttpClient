//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using GitHubOrganization;
//
//    var dick = GitHubOrganization.FromJson(jsonString);

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SampleHttpClient;

public static class Converter
{
    public static readonly JsonSerializerOptions Settings =
        new(JsonSerializerDefaults.General)
        {
            Converters =
            {
                new DateOnlyConverter(),
                new TimeOnlyConverter(),
                IsoDateTimeOffsetConverter.Singleton
            }
        };
}

public class DateOnlyConverter(string? serializationFormat) : JsonConverter<DateOnly>
{
    readonly string _serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    public DateOnlyConverter() : this(null) { }

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            DateOnly.Parse(reader.GetString()!);

    public override void Write(
        Utf8JsonWriter writer,
        DateOnly value,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(_serializationFormat));
}

public class TimeOnlyConverter(string? serializationFormat = null) : JsonConverter<TimeOnly>
{
    readonly string _serializationFormat = serializationFormat ?? "HH:mm:ss.fff";

    public override TimeOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            TimeOnly.Parse(reader.GetString()!);

    public override void Write(
        Utf8JsonWriter writer,
        TimeOnly value,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(_serializationFormat));
}

public class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    //public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

    const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
    CultureInfo? _culture;
    string? _dateTimeFormat;

    public static readonly IsoDateTimeOffsetConverter Singleton = new();

    public CultureInfo Culture
    {
        get => _culture ?? CultureInfo.CurrentCulture;
        set => _culture = value;
    }

    public DateTimeStyles DateTimeStyles { get; set; } = DateTimeStyles.RoundtripKind;

    public string? DateTimeFormat
    {
        get => _dateTimeFormat ?? string.Empty;
        set => _dateTimeFormat = string.IsNullOrEmpty(value) ? null : value;
    }

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateText = reader.GetString();

        return string.IsNullOrEmpty(dateText)
            ? default
            : string.IsNullOrEmpty(_dateTimeFormat)
                ? DateTimeOffset.Parse(dateText, Culture, DateTimeStyles)
                : DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, DateTimeStyles);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        string text;

        if ((DateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
            || (DateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
        {
            value = value.ToUniversalTime();
        }

        text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

        writer.WriteStringValue(text);
    }
}
