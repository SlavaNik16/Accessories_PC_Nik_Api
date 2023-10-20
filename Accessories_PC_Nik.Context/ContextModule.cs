using Accessories_PC_Nik.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;
using TimeTable203.Common;

namespace Accessories_PC_Nik.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddSingleton<IAccessoriesContext, AccessoriesContext>();
        }
    }
}
