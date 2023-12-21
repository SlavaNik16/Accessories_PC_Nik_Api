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

            CreateMap<Client, ClientModel>(MemberList.Destination);

            CreateMap<Component, ComponentModel>(MemberList.Destination);

            CreateMap<Delivery, DeliveryModel>(MemberList.Destination);

            CreateMap<Order, OrderModel>(MemberList.Destination)
                .ForMember(opt => opt.Services, next => next.Ignore())
                .ForMember(opt => opt.Components, next => next.Ignore())
                .ForMember(opt => opt.Delivery, next => next.Ignore())
                .ForMember(opt => opt.Clients, next => next.Ignore());

            CreateMap<Service, ServiceModel>(MemberList.Destination);

            CreateMap<Worker, WorkerModel>(MemberList.Destination)
                .ForMember(opt => opt.Clients, next => next.Ignore());
        }
    }
}
