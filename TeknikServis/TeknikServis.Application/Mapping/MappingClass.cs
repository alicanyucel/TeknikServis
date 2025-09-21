using AutoMapper;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Domain.Entities;

public sealed class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // EF tarafından atanır
            .ForMember(dest => dest.Neighborhood, opt => opt.Ignore()) // Navigation
            .ForMember(dest => dest.Products, opt => opt.Ignore()); // Koleksiyon
    }
}
