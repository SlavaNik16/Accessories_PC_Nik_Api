using Accessories_PC_Nik.Common;
using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Accessories_PC_Nik.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.TryAddScoped<IAccessoriesContext>
                (provider => provider.GetRequiredService<AccessoriesContext>());
            service.TryAddScoped<IDbRead>
                (provider => provider.GetRequiredService<AccessoriesContext>());
            service.TryAddScoped<IDbWriter>
                (provider => provider.GetRequiredService<AccessoriesContext>());
            service.TryAddScoped<IUnitOfWork>
                (provider => provider.GetRequiredService<AccessoriesContext>());
        }
    }
}
