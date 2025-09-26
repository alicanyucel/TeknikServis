public sealed class AppRole : IdentityRole<Guid>
{
    public AppRole() : base() { }

    public AppRole(string roleName) : base()
    {
        Name = roleName;
    }
}
