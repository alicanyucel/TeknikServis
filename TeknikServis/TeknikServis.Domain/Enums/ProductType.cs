using Ardalis.SmartEnum;

namespace TeknikServis.Domain.Enums;

public class ProductType : SmartEnum<ProductType>
{
    public static readonly ProductType Service = new("Hizmet", 1);
    public static readonly ProductType Hardware = new("Donanım", 2);
    public static readonly ProductType Software = new("Yazılım", 3);
    public static readonly ProductType Subscription = new("Abonelik", 4);
    public static readonly ProductType Accessory = new("Aksesuar", 5);

    public ProductType(string name, int value) : base(name, value)
    {
    }
}