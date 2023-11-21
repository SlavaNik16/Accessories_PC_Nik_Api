using Accessories_PC_Nik.Common;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Accessories_PC_Nik.Repositories
{
    public class ReadRepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IReadRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
