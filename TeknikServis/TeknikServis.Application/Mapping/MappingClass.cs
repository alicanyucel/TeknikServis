using AutoMapper;
using TeknikServis.Application.Dtos;
using TeknikServis.Application.Features.Customers.CreateCustomers;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Application.Mapping;
public sealed class MappingClass : Profile
{
    public MappingClass()
    {
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerValue)));

        CreateMap<Customer, CustomerDto>();
    }
}