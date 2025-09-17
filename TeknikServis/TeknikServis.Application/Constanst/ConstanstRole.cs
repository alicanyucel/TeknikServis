using TeknikServis.Domain.Entities;

namespace TeknikServis.Application.Constanst;

public static class ConstantsRole
{
    public static List<AppRole> GetRoles()
    {
        return new List<AppRole>
        {
            new AppRole
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new AppRole
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "User",
                NormalizedName = "USER"
            },
            new AppRole
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            }
        };
    }
}
