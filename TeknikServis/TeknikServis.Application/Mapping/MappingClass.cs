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
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerValue)))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => TimeOnly.FromDateTime(DateTime.Now)))
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => TimeOnly.FromDateTime(DateTime.Now)))
            .ForMember(dest => dest.CreateadAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
        CreateMap<Customer, CustomerDto>().ReverseMap();
    }
}