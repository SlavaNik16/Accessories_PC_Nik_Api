using Microsoft.Extensions.DependencyInjection;

namespace Accessories_PC_Nik.Common
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Метод который по поставленному типу(класс наследуемый от Module - абстрактного класса)
        /// Вызывает метод CreateModule
        /// </summary>
        public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : Module
        {
            var type = typeof(TModule);
            var instance = Activator.CreateInstance(type) as Module;
            instance?.CreateModule(services);
        }
    }
}
