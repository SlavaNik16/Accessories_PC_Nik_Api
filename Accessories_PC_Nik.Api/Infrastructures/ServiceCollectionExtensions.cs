using Accessories_PC_Nik.Common;
using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context;
using Accessories_PC_Nik.Repositories;
using Accessories_PC_Nik.Services;
using Accessories_PC_Nik.Shared;
namespace Accessories_PC_Nik.Api.Infrastructures
{
    /// <summary>
    /// Работа с регистрацией
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрация всех абстракций с имплементацией
        /// </summary>
        public static void AddDependences(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDbWriterContext, DbWriterContext>();
            services.RegisterAutoMapperProfile<ApiProfile>();

            services.RegisterModule<ServiceModule>();
            services.RegisterModule<ReadRepositoryModule>();
            services.RegisterModule<ContextModule>();

            services.RegisterAutoMapper();
        }

    }
}
