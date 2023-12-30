using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.Infrastructures
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(AccessoriesApiTestCollection))]
    public class BaseIntegrationTest
    {
        /// <summary>
        /// Фабрика для работы с бд
        /// </summary>
        protected readonly CustomWebApplicationFactory factory;

        /// <summary>
        /// Контекст бд
        /// </summary>
        protected readonly IAccessoriesContext context;

        /// <summary>
        ///  Сохранение бд
        /// </summary>
        protected readonly IUnitOfWork unitOfWork;


        /// <summary>
        /// Инициализация нового экземпляра <see cref="BaseIntegrationTest"/>
        /// </summary>
        public BaseIntegrationTest(AccessoriesApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;
        }
    }
}
