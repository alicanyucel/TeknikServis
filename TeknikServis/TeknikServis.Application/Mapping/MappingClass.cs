using AutoMapper;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
using TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;

public sealed class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerType)));

        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerType)))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateDocumentLinkCommand, DocumentLink>()
       .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
       .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
       .ForMember(dest => dest.ServiceActionId, opt => opt.MapFrom(src => src.ServiceActionId));

    //    CreateMap<UpdateDocumentLinkCommand, DocumentLink>()
    //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
    //.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
    //.ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(_ => "System"))
    //.ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(_ => TimeOnly.FromDateTime(DateTime.UtcNow)))
    //.ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => (DateTime?)DateTime.UtcNow))
    //.ForAllOtherMembers(opt => opt.Ignore()); // ID, CreatedBy, CreatedTime vs. korunur


    }
}
