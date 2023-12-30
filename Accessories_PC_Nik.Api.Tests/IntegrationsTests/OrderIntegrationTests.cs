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
    /// Тест для контроллера <see cref="OrderController"/>
    /// </summary>
    public class OrderIntegrationTests : BaseIntegrationTest
    {
        /// <summary>
        /// Инициализирую <see cref="OrderIntegrationTests"/>
        /// </summary>
        /// <param name="fixture"></param>
        public OrderIntegrationTests(AccessoriesApiFixture fixture) : base(fixture)
        {

        }

        /// <summary>
        /// Получение запроса всех заказов
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemNew = DataGeneratorRepository.Service();
            await context.Services.AddRangeAsync(serviceItem, serviceItemNew);

            var componentItem = DataGeneratorRepository.Component();
            var componentItemNew = DataGeneratorRepository.Component();
            await context.Components.AddRangeAsync(componentItem, componentItemNew);

            var deliveryItem = DataGeneratorRepository.Delivery();
            var deliveryItemNew = DataGeneratorRepository.Delivery();
            await context.Deliveries.AddRangeAsync(deliveryItem, deliveryItemNew);

            var item = DataGeneratorRepository.Order(x =>
            {
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id;
                x.ComponentId = componentItem.Id;
                x.DeliveryId = deliveryItem.Id;
            });
            var itemDeleted = DataGeneratorRepository.Order(x=>
            { 
                x.DeletedAt = DateTimeOffset.UtcNow; 
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id; 
            });
            await context.Orders.AddRangeAsync(item, itemDeleted);
            await unitOfWork.SaveChangesAsync();
            // Act
            var response = await clientHTTP.GetAsync("/Order");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(resultString);
            result.Should().NotBeNull()
                .And.Contain(x => x.Id == item.Id)
                .And.NotContain(x => x.Id == itemDeleted.Id);
        }

        /// <summary>
        /// Получение запроса заказа по id
        /// </summary>
        [Fact]
        public async Task GetValueById()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemNew = DataGeneratorRepository.Service();
            await context.Services.AddRangeAsync(serviceItem, serviceItemNew);

            var item = DataGeneratorRepository.Order(x =>
            {
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id;
            });
            var itemDeleted = DataGeneratorRepository.Order(x =>
            {
                x.DeletedAt = DateTimeOffset.UtcNow;
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id;
            });
            await context.Orders.AddRangeAsync(item, itemDeleted);
            await unitOfWork.SaveChangesAsync();


            // Act
            var response = await clientHTTP.GetAsync($"/Order/{item.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OrderResponse>(resultString);
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    item.Id,
                    item.Comment,
                    item.OrderTime
                });
        }

        /// <summary>
        /// Добавление заказа
        /// </summary>
        [Fact]
        public async Task PostValueAdd()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemNew = DataGeneratorRepository.Service();
            await context.Services.AddRangeAsync(serviceItem, serviceItemNew);

            var componentItem = DataGeneratorRepository.Component();
            var componentItemNew = DataGeneratorRepository.Component();
            await context.Components.AddRangeAsync(componentItem, componentItemNew);

            var item = DataGeneratorRepository.Order(x =>
            {
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id;
            });
            var itemDeleted = DataGeneratorRepository.Order(x =>
            {
                x.DeletedAt = DateTimeOffset.UtcNow;
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id;
            });
            await context.Orders.AddRangeAsync(item, itemDeleted);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.CreateOrderRequest();
            targetItem.ClientId = clientItemNew.Id;
            targetItem.ComponentId = componentItem.Id;

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PostAsync($"/Order", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OrderResponse>(resultString);

            var orderResult = context.Orders.Single(x => x.Id == result!.Id &&
                                                    x.ClientId == targetItem.ClientId &&
                                                    x.ComponentId == targetItem.ComponentId &&
                                                    x.ServiceId == null);

            orderResult.Should().NotBeNull();
        }


        /// <summary>
        /// Изменение заказа
        /// </summary>
        [Fact]
        public async Task PutValueEdit()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemNew = DataGeneratorRepository.Service();
            await context.Services.AddRangeAsync(serviceItem, serviceItemNew);

            var componentItem = DataGeneratorRepository.Component();
            var componentItemNew = DataGeneratorRepository.Component();
            await context.Components.AddRangeAsync(componentItem, componentItemNew);

            var item = DataGeneratorRepository.Order(x =>
            {
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id;
            });
            var itemNew = DataGeneratorRepository.Order(x =>
            {
                x.ClientId = clientItemNew.Id;
                x.ServiceId = serviceItemNew.Id;
            });
            await context.Orders.AddRangeAsync(item, itemNew);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.EditOrderRequest();
            targetItem.Id = item.Id;
            targetItem.ClientId = clientItemNew.Id;
            targetItem.ComponentId = componentItemNew.Id;


            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PutAsync($"/Order", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OrderResponse>(resultString);

            var orderResult = context.Orders.Single(x => x.Id == result!.Id &&
                                                    x.ClientId == targetItem.ClientId &&
                                                    x.ComponentId == targetItem.ComponentId &&
                                                    x.ServiceId == null);

            orderResult.Should().NotBeNull();
        }


        /// <summary>
        /// Удаление заказа
        /// </summary>
        [Fact]
        public async Task DeleteValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var serviceItem = DataGeneratorRepository.Service();
            var serviceItemNew = DataGeneratorRepository.Service();
            await context.Services.AddRangeAsync(serviceItem, serviceItemNew);

            var item = DataGeneratorRepository.Order(x =>
            {
                x.ClientId = clientItem.Id;
                x.ServiceId = serviceItem.Id;
            });
            var itemNew = DataGeneratorRepository.Order(x =>
            {
                x.ClientId = clientItemNew.Id;
                x.ServiceId = serviceItemNew.Id;
            });
            await context.Orders.AddRangeAsync(item, itemNew);
            await unitOfWork.SaveChangesAsync();
            // Act
            var response = await clientHTTP.DeleteAsync($"/Order/{item.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OrderResponse>(resultString);

            var orderResult = context.Orders.FirstOrDefault(x => x.Id == item.Id);

            orderResult!.DeletedAt.Should().NotBeNull();
        }
    }
}
