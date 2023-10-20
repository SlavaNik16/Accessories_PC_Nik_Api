using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace TimeTable203.Shared
{
    public static class Register
    {
        public static void AssemblyInterfaceAssignableTo<TInterface>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var serviceType = typeof(TInterface);
            var types = serviceType.Assembly.GetTypes()
                .Where(x => serviceType.IsAssignableFrom(x) && !(x.IsAbstract || x.IsInterface));
            foreach (var type in types)
            {
                services.TryAdd(new ServiceDescriptor(type, type, lifetime));
                var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(i => i != typeof(IDisposable) && i.IsPublic && i != serviceType);
                foreach (var interfaceType in interfaces)
                {
                    services.TryAdd(new ServiceDescriptor(interfaceType,
                        provider => provider.GetRequiredService(type),
                        lifetime));
                }
            }
        }
        public static void AddMapper<TProfile>(this IServiceCollection service) where TProfile : Profile
        {
            var type = typeof(TProfile);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var classType in types)
            {
                service.AddAutoMapper(classType);
            }
        }
    }
}
