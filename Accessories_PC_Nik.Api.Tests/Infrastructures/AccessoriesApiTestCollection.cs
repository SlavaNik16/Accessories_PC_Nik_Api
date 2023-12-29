using Xunit;

namespace Accessories_PC_Nik.Api.Tests.Infrastructures
{
    [CollectionDefinition(nameof(AccessoriesApiTestCollection))]
    public class AccessoriesApiTestCollection
        : ICollectionFixture<AccessoriesApiFixture>
    {
    }
}
