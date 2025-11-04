using AutoMapper;
using TeknikServis.Application.Dtos;
using TeknikServis.Application.Features.Customers.CreateCustomer;
using TeknikServis.Application.Features.Customers.UpdateCustomer;
using TeknikServis.Application.Features.DocumentLinks.CreateDcumentLink;
using TeknikServis.Application.Features.DocumentLinks.UpdateDocumentLink;
using TeknikServis.Application.Features.Persons.CreatePerson;
using TeknikServis.Application.Features.Persons.UpdatePerson;
using TeknikServis.Application.Features.Products.CreateProduct;
using TeknikServis.Application.Features.Products.UpdateProduct;
using TeknikServis.Application.Features.ServiceActions.CreateServiceActions;
using TeknikServis.Application.Features.ServiceActions.UpdateServiceAcrions;
using TeknikServis.Application.Features.ServiceLineActions.CreateServiceLineActions;
using TeknikServis.Application.Features.Statuses.CreateSratus;
using TeknikServis.Application.Features.Statuses.UpdateStatus;
using TeknikServis.Application.Features.Users.CreateUser;
using TeknikServis.Application.Features.Users.UpdateUser;
using TeknikServis.Application.Features.VideoLinks.CrateVideoLinks;
using TeknikServis.Application.Features.VideoLinks.UpdateVideoLinks;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Enums;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // AppUser ↔ UserDto
        CreateMap<AppUser, UserDto>().ReverseMap();
        CreateMap<CreateUserCommand, AppUser>().ReverseMap();
        CreateMap<UpdateUserCommand, AppUser>().ReverseMap();

        // Person ↔ Commands
        CreateMap<CreatePersonCommand, Person>()
            .ForMember(dest => dest.ExpertiseArea, opt => opt.MapFrom(src => ExpertiseArea.FromValue(src.ExpertiseArea)))
            .ForMember(dest => dest.Actions, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<UpdatePersonCommand, Person>()
            .ForMember(dest => dest.ExpertiseArea, opt => opt.MapFrom(src => ExpertiseArea.FromValue(src.ExpertiseArea)))
            .ForMember(dest => dest.Actions, opt => opt.Ignore())
            .ReverseMap();

        // Product ↔ Commands
        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => ProductType.FromValue(src.ProductType)));

        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => ProductType.FromValue(src.ProductType)))
            .ReverseMap();
        // DocumentLinkdto
        CreateMap<DocumentLink, DocumentLinkUploadDto>().ReverseMap();
        //videolkn vedolink to
        CreateMap<VideoLink, VideoLinkUploadDto>().ReverseMap();
        // Status ↔ Commands
        CreateMap<CreateStatusCommand, Status>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ReverseMap();

        CreateMap<UpdateStatusCommand, Status>().ReverseMap();

        // ServiceAction ↔ Commands
        CreateMap<CreateServiceActionCommand, ServiceAction>().ReverseMap();
        CreateMap<UpdateServiceActionCommand, ServiceAction>().ReverseMap();

        // ServiceLineAction
        CreateMap<CreateServiceLineActionsCommand, ServiceLineAction>().ReverseMap();

        // VideoLink ↔ Commands
        CreateMap<CreateVideoLinkCommand, VideoLink>().ReverseMap();
        CreateMap<UpdateVideoLinkCommand, VideoLink>().ReverseMap();

        // Customer ↔ Commands
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerType)))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
            .ForMember(dest => dest.ProvinceId, opt => opt.MapFrom(src => src.ProvinceId))
            .ForMember(dest => dest.DistrictId, opt => opt.MapFrom(src => src.DistrictId));

        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CustomerType, opt => opt.MapFrom(src => CustomerType.FromValue(src.CustomerType)))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        // DocumentLink ↔ Commands
        CreateMap<CreateDocumentLinkCommand, DocumentLink>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ServiceActionId, opt => opt.MapFrom(src => src.ServiceActionId));

        CreateMap<UpdateDocumentLinkCommand, DocumentLink>().ReverseMap();
    }
}