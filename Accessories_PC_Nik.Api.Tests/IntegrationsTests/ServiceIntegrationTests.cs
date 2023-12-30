using Accessories_PC_Nik.Api.Controllers;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.Tests.Infrastructures;
using Accessories_PC_Nik.Tests.Generator;
using FluentAssertions;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests.IntegrationsTests
{
    /// <summary>
    /// Тест для контроллера <see cref="ServicesController"/>
    /// </summary>
    public class ServiceIntegrationTests : BaseIntegrationTest
    {
        /// <summary>
        ///  Инициализирую <see cref="ServiceIntegrationTests"/>
        /// </summary>
        /// <param name="fixture"></param>
        public ServiceIntegrationTests(AccessoriesApiFixture fixture) : base(fixture)
        {


        }

        /// <summary>
        /// Получение запроса всех услуг
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemDeleted = DataGeneratorRepository.Service(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Services.AddRangeAsync(serviceItem, serviceItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync("/Services");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ServicesResponse>>(resultString);
            result.Should().NotBeNull()
                .And.ContainSingle(x => x.Id == serviceItem.Id)
                .And.NotContain(x => x.Id == serviceItemDeleted.Id);
        }

        /// <summary>
        /// Получение запроса услуги по id
        /// </summary>
        [Fact]
        public async Task GetValueById()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemDeleted = DataGeneratorRepository.Service(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Services.AddRangeAsync(serviceItem, serviceItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync($"/Services/{serviceItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ServicesResponse>(resultString);
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    serviceItem.Id,
                });
        }

        /// <summary>
        /// Добавление услуги
        /// </summary>
        [Fact]
        public async Task PostValueAdd()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemDeleted = DataGeneratorRepository.Service(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Services.AddRangeAsync(serviceItem, serviceItemDeleted);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.CreateComponentRequest();

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PostAsync("/Services", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ServicesResponse>(resultString);

            var serviceResult = context.Services.Single(x => x.Id == result!.Id &&
                                                    x.Name == targetItem.Name);

            serviceResult.Should().NotBeNull();
        }


        /// <summary>
        /// Изменения услуги
        /// </summary>
        [Fact]
        public async Task PutValueEdit()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemNew = DataGeneratorRepository.Service();
            await context.Services.AddRangeAsync(serviceItem, serviceItemNew);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.EditClientRequest();
            targetItem.Id = serviceItem.Id;

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PutAsync($"/Services", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ServicesResponse>(resultString);

            var serviceResult = context.Services.Single(x => x.Id == targetItem.Id
                                                        && x.Name == targetItem.Name);

            serviceResult.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление услуги
        /// </summary>
        [Fact]
        public async Task DeleteValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemNew = DataGeneratorRepository.Service();
            await context.Services.AddRangeAsync(serviceItem, serviceItemNew);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.DeleteAsync($"/Services/{serviceItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ServicesResponse>(resultString);

            var  serviceResult = context.Services.FirstOrDefault(x => x.Id == serviceItem.Id);

            serviceResult!.DeletedAt.Should().NotBeNull();
        }

    }
}

