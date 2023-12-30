using Accessories_PC_Nik.Api.Controllers;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.Tests.Infrastructures;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Tests.Generator;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.IntegrationsTests
{
    /// <summary>
    /// Тест для контроллера <see cref="ClientsController"/>
    /// </summary>
    public class ClientIntegrationTests : BaseIntegrationTest
    {
        /// <summary>
        /// Инициализирую <see cref="ClientIntegrationTests"/>
        /// </summary>
        /// <param name="fixture"></param>
        public ClientIntegrationTests(AccessoriesApiFixture fixture) : base(fixture)
        {

        }

        /// <summary>
        /// Получение запроса всех клиентов
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemDeleted = DataGeneratorRepository.Client(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Clients.AddRangeAsync(clientItem, clientItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync("/Clients");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ClientsResponse>>(resultString);
            result.Should().NotBeNull()
                .And.ContainSingle(x => x.Id == clientItem.Id)
                .And.NotContain(x=>x.Id == clientItemDeleted.Id);
        }

        /// <summary>
        /// Получение запроса клиента по id
        /// </summary>
        [Fact]
        public async Task GetValueById()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            await context.Clients.AddAsync(clientItem);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync($"/Clients/{clientItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ClientsResponse>(resultString);
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    clientItem.Id,
                });
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        [Fact]
        public async Task PostValueAdd()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            await context.Clients.AddAsync(clientItem);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.CreateClientRequest();

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PostAsync($"/Clients", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ClientsResponse>(resultString);

            var clientResult = context.Clients.Single(x => x.Id == result!.Id &&
                                                    x.Surname == targetItem.Surname &&
                                                    x.Phone == targetItem.Phone);

            clientResult.Should().NotBeNull();
        }

       
        /// <summary>
        /// Изменения клиента
        /// </summary>
        [Fact]
        public async Task PutValueEdit()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.EditClientRequest();
            targetItem.Id = clientItem.Id;

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PutAsync($"/Clients", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ClientsResponse>(resultString);

            var clientResult = context.Clients.Single(x => x.Id == targetItem.Id && 
                                                      x.Surname == targetItem.Surname &&
                                                      x.Phone == targetItem.Phone);

            clientResult.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        [Fact]
        public async Task DeleteValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            await context.Clients.AddAsync(clientItem);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.DeleteAsync($"/Clients/{clientItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ClientsResponse>(resultString);

            var clientResult = context.Clients.FirstOrDefault(x => x.Id == clientItem.Id);

            clientResult!.DeletedAt.Should().NotBeNull();
        }

    }
}
