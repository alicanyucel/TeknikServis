using Ardalis.SmartEnum;

namespace TeknikServis.Domain.Enums;

public sealed class ExpertiseArea : SmartEnum<ExpertiseArea, int>
{
    public static readonly ExpertiseArea Electronics = new("Elektronik", 1);
    public static readonly ExpertiseArea Mechanics = new("Mekanik", 2);
    public static readonly ExpertiseArea Software = new("Yazılım", 3);
    public static readonly ExpertiseArea Networking = new("Ağ", 4);
    private ExpertiseArea(string name, int value) : base(name, value) { }
}
