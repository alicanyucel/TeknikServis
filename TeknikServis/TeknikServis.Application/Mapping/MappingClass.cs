using AutoMapper;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
using TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;
using TeknikServis.Application.Features.Products.CreateProduct;
using TeknikServis.Application.Features.ServiceActions.CreateServiceActions;
using TeknikServis.Application.Features.Statuses.CreateSratus;
using TeknikServis.Application.Features.Statuses.UpdateStatus;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;

public sealed class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateProductCommand, Product>().ReverseMap()
        .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => ProductType.FromValue(src.ProductType)));
        CreateMap<UpdateStatusCommand, Status>().ReverseMap();
        CreateMap<CreateStatusCommand, Status>().ReverseMap();  
        CreateMap<CreateServiceActionCommand, ServiceAction>().ReverseMap();
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



    }
}
