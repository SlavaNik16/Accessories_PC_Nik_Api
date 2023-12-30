using Xunit;

namespace Accessories_PC_Nik.Api.Tests.Infrastructures
{
    /// <summary>
    /// Класс для работы с бд, помогающая в тестирование 
    /// </summary>
    [CollectionDefinition(nameof(AccessoriesApiTestCollection))]
    public class AccessoriesApiTestCollection
        : ICollectionFixture<AccessoriesApiFixture>
    {
    }
}
