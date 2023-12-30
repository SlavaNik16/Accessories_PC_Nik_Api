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
        protected readonly CustomWebApplicationFactory factory;
        protected readonly IAccessoriesContext context;
        protected readonly IUnitOfWork unitOfWork;


        public BaseIntegrationTest(AccessoriesApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;
        }
    }
}
