public sealed class Customer : Entity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string AddressLine { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public int NeighborhoodId { get; set; }
    public Neighborhood Neighborhood { get; set; }
    public CustomerType CustomerType { get; set; }
    public ICollection<Product> Products { get; set; }
}
