using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TeknikServis.Domain.Entities;

namespace TeknikServis.Application.Features.LocationSenders;

public class LocationSeeder
{
    private readonly DbContext _context;

    public LocationSeeder(DbContext context)
    {
        _context = context;
    }

    public async Task SeedFromJsonAsync(string jsonPath, string countryName = "TÜRKİYE", CancellationToken cancellationToken = default)
    {
        await using var fileStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(fileStream);
        var json = await reader.ReadToEndAsync();

        // Heuristic: detect flat schema by key names present in raw JSON
        var isFlatSchema = json.IndexOf("\"SEHIRADI\"", StringComparison.OrdinalIgnoreCase) >= 0
                           || json.IndexOf("\"ILCEADI\"", StringComparison.OrdinalIgnoreCase) >= 0
                           || json.IndexOf("\"MAHALLEADI\"", StringComparison.OrdinalIgnoreCase) >= 0;

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        if (isFlatSchema)
        {
            var rows = JsonSerializer.Deserialize<List<FlatRow>>(json, options) ?? new();
            var rowCountryName = rows.FirstOrDefault()?.ULKE?.Trim();
            if (!string.IsNullOrWhiteSpace(rowCountryName))
                countryName = rowCountryName!;
            await SeedFlatAsync(rows, countryName, cancellationToken);
            return;
        }
        else
        {
            var provinces = JsonSerializer.Deserialize<List<JsonProvince>>(json, options) ?? new();
            // Additional guard: ensure it's truly nested (has at least one named province)
            if (provinces.Any(p => !string.IsNullOrWhiteSpace(p.name)))
            {
                await SeedNestedAsync(provinces, countryName, cancellationToken);
                return;
            }
        }
        // If nothing matched, do nothing
    }

    private async Task SeedNestedAsync(List<JsonProvince> provinces, string countryName, CancellationToken ct)
    {
        var countryId = await EnsureCountryAsync(countryName, ct);
        var now = DateTime.UtcNow;
        var createdTime = TimeOnly.FromDateTime(now);

        foreach (var p in provinces)
        {
            var province = new Province
            {
                Name = p.name?.Trim() ?? string.Empty,
                Code = p.code ?? 0,
                Ref = p.@ref ?? 0,
                CountryId = countryId,
                CreatedBy = "seeder",
                UpdatedBy = "seeder",
                CreateadAt = now,
                CreatedTime = createdTime,
                UpdatedAt = now,
                UpdatedTime = createdTime,
                Districts = new List<District>()
            };

            if (p.districts != null)
            {
                foreach (var d in p.districts)
                {
                    var district = new District
                    {
                        Name = d.name?.Trim() ?? string.Empty,
                        PostalCode = d.postalCode ?? string.Empty,
                        Code = d.code ?? 0,
                        Ref = d.@ref ?? 0,
                        CreatedBy = "seeder",
                        UpdatedBy = "seeder",
                        CreateadAt = now,
                        CreatedTime = createdTime,
                        UpdatedAt = now,
                        UpdatedTime = createdTime,
                        Neighbourhoods = new List<Neighbourhood>()
                    };

                    if (d.neighborhoods != null)
                    {
                        foreach (var n in d.neighborhoods)
                        {
                            if (district.Neighbourhoods.Any(x => x.Name.Equals(n, StringComparison.OrdinalIgnoreCase)))
                                continue;

                            district.Neighbourhoods.Add(new Neighbourhood
                            {
                                Name = (n ?? string.Empty).Trim(),
                                Nr = 0,
                                Code = 0,
                                CreatedBy = "seeder",
                                UpdatedBy = "seeder",
                                CreateadAt = now,
                                CreatedTime = createdTime,
                                UpdatedAt = now,
                                UpdatedTime = createdTime
                            });
                        }
                    }

                    province.Districts.Add(district);
                }
            }

            _context.Set<Province>().Add(province);
        }

        await _context.SaveChangesAsync(ct);
    }

    private async Task SeedFlatAsync(List<FlatRow> rows, string countryName, CancellationToken ct)
    {
        if (rows.Count == 0) return;

        var countryId = await EnsureCountryAsync(countryName, ct);
        var now = DateTime.UtcNow;
        var createdTime = TimeOnly.FromDateTime(now);

        // Preload existing provinces for the country to reduce queries
        var existingProvinces = await _context.Set<Province>()
            .Where(p => p.CountryId == countryId)
            .ToListAsync(ct);

        var provinceCache = existingProvinces
            .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

        foreach (var r in rows)
        {
            var provinceName = (r.SEHIRADI ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(provinceName)) continue;

            if (!provinceCache.TryGetValue(provinceName, out var province))
            {
                var provinceCode = ParseInt(r.SEHIRKODU);
                province = existingProvinces.FirstOrDefault(p => p.Code == provinceCode && p.CountryId == countryId);
                if (province is null)
                {
                    province = new Province
                    {
                        Name = provinceName,
                        Code = provinceCode,
                        Ref = r.SEHIRREF ?? 0,
                        CountryId = countryId,
                        CreatedBy = "seeder",
                        UpdatedBy = "seeder",
                        CreateadAt = now,
                        CreatedTime = createdTime,
                        UpdatedAt = now,
                        UpdatedTime = createdTime,
                        Districts = new List<District>()
                    };
                    _context.Set<Province>().Add(province);
                    existingProvinces.Add(province);
                }
                provinceCache[provinceName] = province;
            }

            var districtName = (r.ILCEADI ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(districtName)) continue;

            var district = province.Districts.FirstOrDefault(d => d.Name.Equals(districtName, StringComparison.OrdinalIgnoreCase));
            if (district is null)
            {
                if (province.Id != 0)
                {
                    district = await _context.Set<District>()
                        .Where(d => d.ProvinceId == province.Id && d.Name == districtName)
                        .FirstOrDefaultAsync(ct);
                }

                district ??= new District
                {
                    Name = districtName,
                    Code = ParseInt(r.ILCEKODU),
                    Ref = r.ILCEID ?? 0,
                    PostalCode = r.POSTAKODU?.ToString() ?? string.Empty,
                    CreatedBy = "seeder",
                    UpdatedBy = "seeder",
                    CreateadAt = now,
                    CreatedTime = createdTime,
                    UpdatedAt = now,
                    UpdatedTime = createdTime,
                    Neighbourhoods = new List<Neighbourhood>()
                };

                province.Districts.Add(district);
            }

            if (!string.IsNullOrWhiteSpace(r.MAHALLEADI))
            {
                var nhName = r.MAHALLEADI!.Trim();
                if (!district.Neighbourhoods.Any(n => n.Name.Equals(nhName, StringComparison.OrdinalIgnoreCase)))
                {
                    district.Neighbourhoods.Add(new Neighbourhood
                    {
                        Name = nhName,
                        Nr = r.MAHALLENR ?? 0,
                        Code = ParseInt(r.MAHALLEKODU),
                        CreatedBy = "seeder",
                        UpdatedBy = "seeder",
                        CreateadAt = now,
                        CreatedTime = createdTime,
                        UpdatedAt = now,
                        UpdatedTime = createdTime
                    });
                }
            }
        }

        await _context.SaveChangesAsync(ct);
    }

    private static int ParseInt(object? s)
    {
        if (s == null) return 0;
        if (s is int i) return i;
        if (s is long l) return (int)l;
        if (s is string str)
        {
            if (int.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var v)) return v;
        }
        return 0;
    }

    private async Task<int> EnsureCountryAsync(string countryName, CancellationToken ct)
    {
        var country = await _context.Set<Country>().FirstOrDefaultAsync(c => c.Name == countryName, ct);
        if (country is null)
        {
            country = new Country
            {
                Name = countryName,
                Code = 792,
                Nr = 90,
                CreatedBy = "seeder",
                UpdatedBy = "seeder",
                CreateadAt = DateTime.UtcNow,
                CreatedTime = TimeOnly.FromDateTime(DateTime.UtcNow),
                UpdatedAt = DateTime.UtcNow,
                UpdatedTime = TimeOnly.FromDateTime(DateTime.UtcNow)
            };
            _context.Set<Country>().Add(country);
            await _context.SaveChangesAsync(ct);
        }
        return country.Id;
    }

    // Nested schema models
    private class JsonProvince
    {
        public string name { get; set; } = string.Empty;
        public int? code { get; set; }
        public int? @ref { get; set; }
        public List<JsonDistrict> districts { get; set; } = new();
    }

    private class JsonDistrict
    {
        public string name { get; set; } = string.Empty;
        public int? code { get; set; }
        public int? @ref { get; set; }
        public string? postalCode { get; set; }
        public List<string> neighborhoods { get; set; } = new();
    }

    private sealed class FlatRow
    {
        [JsonPropertyName("ULKENR")] public int? ULKENR { get; set; }
        [JsonPropertyName("ULKEKODU")] public string? ULKEKODU { get; set; }
        [JsonPropertyName("ULKE")] public string? ULKE { get; set; }
        [JsonPropertyName("SEHIRREF")] public int? SEHIRREF { get; set; }
        [JsonPropertyName("SEHIRKODU")] public object? SEHIRKODU { get; set; }
        [JsonPropertyName("SEHIRADI")] public string? SEHIRADI { get; set; }
        [JsonPropertyName("ILCEID")] public int? ILCEID { get; set; }
        [JsonPropertyName("ILCEKODU")] public object? ILCEKODU { get; set; }
        [JsonPropertyName("ILCEADI")] public string? ILCEADI { get; set; }
        [JsonPropertyName("MAHALLENR")] public int? MAHALLENR { get; set; }
        [JsonPropertyName("MAHALLEKODU")] public object? MAHALLEKODU { get; set; }
        [JsonPropertyName("MAHALLEADI")] public string? MAHALLEADI { get; set; }
        [JsonPropertyName("POSTAKODU")] public object? POSTAKODU { get; set; }
    }
}