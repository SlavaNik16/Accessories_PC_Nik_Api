using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.Infrastructures
{
    /// <summary>
    /// Класс для создание бд и удаление ее
    /// </summary>
    public class AccessoriesApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private AccessoriesContext? accessoriesContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AccessoriesApiFixture"/>
        /// </summary>
        public AccessoriesApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => AccessoriesContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await AccessoriesContext.Database.EnsureDeletedAsync();
            await AccessoriesContext.Database.CloseConnectionAsync();
            await AccessoriesContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        /// <summary>
        /// Фабрика работы с бд 
        /// </summary>
        public CustomWebApplicationFactory Factory => factory;

        /// <summary>
        /// Контекст бд
        /// </summary>
        public IAccessoriesContext Context => AccessoriesContext;

        /// <summary>
        /// Контекст бд сохранения
        /// </summary>
        public IUnitOfWork UnitOfWork => AccessoriesContext;

        internal AccessoriesContext AccessoriesContext
        {
            get
            {
                if (accessoriesContext != null)
                {
                    return accessoriesContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                accessoriesContext = scope.ServiceProvider.GetRequiredService<AccessoriesContext>();
                return accessoriesContext;
            }
        }
    }
}
