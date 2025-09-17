using AutoMapper;
using TeknikServis.Application.Features.Customers.CreateCustomers;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;

namespace TeknikServis.Application.Mapping;
public sealed class MappingClass : Profile
{
    public MappingClass()
    {

        CreateMap<CreateCustomerCommand, Customer>().ForMember(member => member.CustomerType, options =>
        {
            options.MapFrom(map => CustomerType.FromValue(map.CustomerValue));
        });
    }
}