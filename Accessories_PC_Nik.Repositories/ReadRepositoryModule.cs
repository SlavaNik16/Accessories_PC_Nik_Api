using Accessories_PC_Nik.Repositories.Anchors;
using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;
using TimeTable203.Shared;

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
