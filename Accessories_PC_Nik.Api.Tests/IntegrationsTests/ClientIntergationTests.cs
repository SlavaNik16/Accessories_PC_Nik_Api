using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.Tests.Infrastructures;
using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Services.Tests;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests
{
    [Collection(nameof(AccessoriesApiTestCollection))]
    public class ClientIntergationTests
    {
        private readonly CustomWebApplicationFactory factory;
        private readonly IAccessoriesContext context;
        private readonly IUnitOfWork unitOfWork;

        public ClientIntergationTests(AccessoriesApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            // Arrange
            var client = factory.CreateClient();
            var targetItem = TestDataGeneratorApi.Client();
            await context.Clients.AddAsync(targetItem);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Clients");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ClientsResponse>>(resultString);
            result.Should().NotBeNull()
                .And.ContainSingle(x => x.Id == targetItem.Id);
        }
    }
}
