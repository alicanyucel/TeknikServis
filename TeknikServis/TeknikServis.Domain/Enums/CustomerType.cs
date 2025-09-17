using Ardalis.SmartEnum;

namespace TeknikServis.Domain.Enums;

public sealed class CustomerType : SmartEnum<CustomerType>
{
    public static readonly CustomerType Invidual = new("Bireysel", 1);
    public static readonly CustomerType Corporate = new("Kurumsal", 2);
    public CustomerType(string name, int value) : base(name, value)
    {
    }
}
