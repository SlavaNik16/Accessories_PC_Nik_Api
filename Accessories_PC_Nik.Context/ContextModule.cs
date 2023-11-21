using Accessories_PC_Nik.Common;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Accessories_PC_Nik.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddScoped<IAccessoriesContext, AccessoriesContext>();
        }
    }
}
