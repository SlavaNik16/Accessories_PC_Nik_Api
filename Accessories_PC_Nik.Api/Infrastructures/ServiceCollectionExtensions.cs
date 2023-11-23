using Accessories_PC_Nik.Context;
using Accessories_PC_Nik.Repositories;
using Accessories_PC_Nik.Services;
using Accessories_PC_Nik.Common;
using Accessories_PC_Nik.Common.Entity;
using Accessories_PC_Nik.Shared;
namespace Accessories_PC_Nik.Api.Infrastructures
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependences(this IServiceCollection services)
        {
            services.RegisterModule<ServiceModule>();
            services.RegisterModule<ReadRepositoryModule>();
            services.RegisterModule<ContextModule>();

            services.RegisterAutoMapper();
        }

    }
}
