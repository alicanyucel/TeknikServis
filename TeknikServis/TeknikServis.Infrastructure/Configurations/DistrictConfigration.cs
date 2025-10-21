using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeknikServis.Domain.Entities;

namespace TeknikServis.Infrastructure.Configurations;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasData(
            DistrictConstants.Districts.Select(d => new
            {
                d.Id,
                d.Code,
                d.Ref,
                d.Name,
                d.PostalCode,
                d.ProvinceId
            }).ToArray()
        );
    }
}
