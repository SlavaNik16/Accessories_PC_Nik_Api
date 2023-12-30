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
    /// Тест для контроллера <see cref="DeliveryController"/>
    /// </summary>
    public class DeliveryIntegrationTests : BaseIntegrationTest
    {
        /// <summary>
        ///  Инициализирую <see cref="DeliveryIntegrationTests"/>
        /// </summary>
        /// <param name="fixture"></param>
        public DeliveryIntegrationTests(AccessoriesApiFixture fixture) : base(fixture)
        {


        }

        /// <summary>
        /// Получение запроса всех доставок
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var deliveryItem = DataGeneratorRepository.Delivery();
            var deliveryItemDeleted = DataGeneratorRepository.Delivery(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Deliveries.AddRangeAsync(deliveryItem, deliveryItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync("/Delivery");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<DeliveryResponse>>(resultString);
            result.Should().NotBeNull()
                .And.ContainSingle(x => x.Id == deliveryItem.Id)
                .And.NotContain(x => x.Id == deliveryItemDeleted.Id);
        }

        /// <summary>
        /// Получение запроса достаки по id
        /// </summary>
        [Fact]
        public async Task GetValueById()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var deliveryItem = DataGeneratorRepository.Delivery();
            var deliveryItemDeleted = DataGeneratorRepository.Delivery(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Deliveries.AddRangeAsync(deliveryItem, deliveryItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync($"/Delivery/{deliveryItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DeliveryResponse>(resultString);
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    deliveryItem.Id,
                });
        }

        /// <summary>
        /// Добавление доставки
        /// </summary>
        [Fact]
        public async Task PostValueAdd()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var deliveryItem = DataGeneratorRepository.Delivery();
            var deliveryItemDeleted = DataGeneratorRepository.Delivery(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Deliveries.AddRangeAsync(deliveryItem, deliveryItemDeleted);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.CreateDeliveryRequest();

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PostAsync("/Delivery", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DeliveryResponse>(resultString);

            var deliveryResult = context.Deliveries.Single(x => x.Id == result!.Id &&
                                                    x.From == targetItem.From &&
                                                    x.To == targetItem.To);

            deliveryResult.Should().NotBeNull();
        }


        /// <summary>
        /// Изменения доставки
        /// </summary>
        [Fact]
        public async Task PutValueEdit()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var deliveryItem = DataGeneratorRepository.Delivery();
            var deliveryItemNew = DataGeneratorRepository.Delivery();
            await context.Deliveries.AddRangeAsync(deliveryItem, deliveryItemNew);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.EditDeliveryRequest();
            targetItem.Id = deliveryItem.Id;

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PutAsync($"/Delivery", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ServicesResponse>(resultString);

            var deliveryResult = context.Deliveries.Single(x => x.Id == result!.Id &&
                                                    x.From == targetItem.From &&
                                                    x.To == targetItem.To);

            deliveryResult.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление доставки
        /// </summary>
        [Fact]
        public async Task DeleteValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var deliveryItem = DataGeneratorRepository.Delivery();
            var deliveryItemNew = DataGeneratorRepository.Delivery();
            await context.Deliveries.AddRangeAsync(deliveryItem, deliveryItemNew);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.DeleteAsync($"/Delivery/{deliveryItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ServicesResponse>(resultString);

            var deliveryResult = context.Deliveries.FirstOrDefault(x => x.Id == deliveryItem.Id);

            deliveryResult!.DeletedAt.Should().NotBeNull();
        }

    }
}
