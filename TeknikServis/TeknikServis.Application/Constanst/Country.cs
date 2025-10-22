using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Application.Constanst;

public static class CountryConstants
{
    public static readonly IReadOnlyList<Country> Countries = new List<Country>
{
    new Country { Id = 179, Code = "TR", Name = "TÜRKİYE", NormalizedName = "TURKIYE" },
    new Country { Id = 840, Code = "US", Name = "UNITED STATES", NormalizedName = "UNITED STATES" },
    new Country { Id = 276, Code = "DE", Name = "GERMANY", NormalizedName = "GERMANY" },
    new Country { Id = 250, Code = "FR", Name = "FRANCE", NormalizedName = "FRANCE" },
    new Country { Id = 826, Code = "GB", Name = "UNITED KINGDOM", NormalizedName = "UNITED KINGDOM" },
    new Country { Id = 124, Code = "CA", Name = "CANADA", NormalizedName = "CANADA" },
    new Country { Id = 392, Code = "JP", Name = "JAPAN", NormalizedName = "JAPAN" },
    new Country { Id = 156, Code = "CN", Name = "CHINA", NormalizedName = "CHINA" },
    new Country { Id = 76,  Code = "BR", Name = "BRAZIL", NormalizedName = "BRAZIL" },
    new Country { Id = 36,  Code = "AU", Name = "AUSTRALIA", NormalizedName = "AUSTRALIA" }
};
}
