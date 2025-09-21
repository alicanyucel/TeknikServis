using AutoMapper;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;

public sealed class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerType)))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(_ => "System"))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(_ => "System"))
            .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(_ => TimeOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(_ => TimeOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(dest => dest.CreateadAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (DateTime?)DateTime.UtcNow))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerType)))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(_ => "System"))
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(_ => TimeOnly.FromDateTime(DateTime.UtcNow)))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (DateTime?)DateTime.UtcNow))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
