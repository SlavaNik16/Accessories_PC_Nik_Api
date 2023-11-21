using Accessories_PC_Nik.Common;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Automappers;
using Accessories_PC_Nik.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Accessories_PC_Nik.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);

            service.RegisterAutoMapperProfile<ServiceProfile>();
        }
    }
}
