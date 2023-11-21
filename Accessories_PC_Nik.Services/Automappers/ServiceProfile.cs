using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Services.Contracts.Enums;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;
using AutoMapper.Extensions.EnumMapping;

namespace Accessories_PC_Nik.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            CreateMap<AccessLevelTypes, AccessLevelTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
            CreateMap<DocumentTypes, DocumentTypesModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
            CreateMap<MaterialType, MaterialTypeModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
            CreateMap<TypeComponents, TypeComponentsModel>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<AccessKey, AccessKeyModel>(MemberList.Destination);

            CreateMap<Client, ClientsModel>(MemberList.Destination);

            CreateMap<Component, ComponentsModel>(MemberList.Destination);

            CreateMap<Delivery, DeliveryModel>(MemberList.Destination);

            CreateMap<Order, OrderModel>(MemberList.Destination)
                .ForMember(pref => pref.Services, next => next.Ignore())
                .ForMember(pref => pref.Components, next => next.Ignore())
                .ForMember(pref => pref.Delivery, next => next.Ignore())
                .ForMember(pref => pref.Clients, next => next.Ignore());

            CreateMap<Service, ServicesModel>(MemberList.Destination);

            CreateMap<Worker, WorkersModel>(MemberList.Destination)
                .ForMember(pref => pref.Clients, next => next.Ignore());
        }
    }
}
