using TeknikServis.Domain.Entities;

namespace TeknikServis.Application.Constanst;

public static class CountryConstants
{
    public static readonly Country Türkiye = new()
    {
        Id = 1,
        Name = "Türkiye",
        Code = "Tr",
        CreatedTime = new TimeOnly(0, 0),
        UpdatedTime = new TimeOnly(0, 0),
        CreatedBy = "system",
        UpdatedBy = "system",
        CreateadAt = DateTime.UtcNow
    };
}