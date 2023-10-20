using Microsoft.Extensions.DependencyInjection;

namespace TimeTable203.Common
{
    public abstract class Module
    {
        /// <summary>
        /// Создаёт зависимости
        /// </summary>
        public abstract void CreateModule(IServiceCollection service);
    }
}