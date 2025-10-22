using TeknikServis.Domain.Entities;

namespace TeknikServis.Application.Constanst;

public static class CountryConstants
{
    public static readonly IReadOnlyList<Country> Countries = new[]
    {
        new Country
        {
            Id = 1,
            Code = 792,
            Nr = 90,
            Name = "TÜRKİYE",
            CreatedTime = TimeOnly.MinValue,
            UpdatedTime =TimeOnly.MinValue,
            CreatedBy = string.Empty,
            UpdatedBy = string.Empty,
            CreateadAt = DateTime.MinValue
        },

    };
}
