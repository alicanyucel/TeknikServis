using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TeknikServis.WebAPI.JsonConverters;

public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private static readonly string[] Formats =
    {
        "HH:mm",
        "HH:mm:ss",
        "HH:mm:ss.fff",
        "HH:mm:ss.fffffff",
        "HH:mm:ssZ",
        "HH:mm:ss.fffZ",
        "HH:mm:ss.fffffffZ"
    };

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString() ?? throw new JsonException();

        // 1) Exact time formats (with/without fractional seconds, with optional Z)
        if (TimeOnly.TryParseExact(s, Formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out var t))
            return t;

        // 2) ISO 8601 full datetime (e.g. 2025-11-04T08:30:00.000Z)
        if (DateTimeOffset.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AdjustToUniversal, out var dto))
            return TimeOnly.FromDateTime(dto.UtcDateTime);
        if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dt))
            return TimeOnly.FromDateTime(dt);

        // 3) Timespan-like inputs ("08:30:00", "08:30")
        if (TimeSpan.TryParse(s, CultureInfo.InvariantCulture, out var ts))
            return TimeOnly.FromTimeSpan(ts);

        // 4) Fallback plain parse
        if (TimeOnly.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out t))
            return t;

        throw new JsonException($"Cannot parse TimeOnly from '{s}'. Supported formats: HH:mm[:ss][.fffffff][Z] or ISO date-time.");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
}
