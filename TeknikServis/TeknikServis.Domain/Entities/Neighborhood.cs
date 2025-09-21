using System;
using System.Collections.Generic;
using TeknikServis.Domain.Abstractions;

namespace TeknikServis.Domain.Entities
{
    public class Neighborhood : Entity
    {
        public string Name { get; set; } = string.Empty;
        public Guid DistrictId { get; set; }
        public District District { get; set; } = null!;
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
