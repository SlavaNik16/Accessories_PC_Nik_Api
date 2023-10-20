using Accessories_PC_Nik.Context;
using Accessories_PC_Nik.Repositories;
using Accessories_PC_Nik.Services;
using Microsoft.OpenApi.Models;

namespace Accessories_PC_Nik.Api.Infrastructures
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependences(this IServiceCollection services)
        {
            services.RegisterModule<ServiceModule>();
            services.RegisterModule<ReadRepositoryModule>();
            services.RegisterModule<ContextModule>();
        }
        public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : TimeTable203.Common.Module
        {
            var type = typeof(TModule);
            var instance = Activator.CreateInstance(type) as TimeTable203.Common.Module;
            if (instance == null)
            {
                return;
            }
            instance.CreateModule(services);
        }
        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Clients", new OpenApiInfo { Title = "Сущность клиентов", Version = "v1" });
                c.SwaggerDoc("Workers", new OpenApiInfo { Title = "Сущность работника", Version = "v1" });
                c.SwaggerDoc("Components", new OpenApiInfo { Title = "Сущность компонентов", Version = "v1" });
                c.SwaggerDoc("Services", new OpenApiInfo { Title = "Сущность услуг", Version = "v1" });
                c.SwaggerDoc("Delivery", new OpenApiInfo { Title = "Сущность доставки", Version = "v1" });
                c.SwaggerDoc("Order", new OpenApiInfo { Title = "Сущность заказов", Version = "v1" });
            });
        }
        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Clients/swagger.json", "Клиенты");
                x.SwaggerEndpoint("Workers/swagger.json", "Работники");
                x.SwaggerEndpoint("Components/swagger.json", "Компоненты");
                x.SwaggerEndpoint("Services/swagger.json", "Услуги");
                x.SwaggerEndpoint("Delivery/swagger.json", "Доставка");
                x.SwaggerEndpoint("Order/swagger.json", "Заказы");
            });
        }
    }
}
