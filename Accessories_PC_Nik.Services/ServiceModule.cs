using Accessories_PC_Nik.Services.Anchors;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Shared;

namespace Accessories_PC_Nik.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);

            service.AddMapper<Profile>();
        }
    }
}
