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
    /// Тест для контроллера <see cref="ComponentsController"/>
    /// </summary>
    public class ComponentIntegrationTests : BaseIntegrationTest
    {
        /// <summary>
        ///  Инициализирую <see cref="ComponentIntegrationTests"/>
        /// </summary>
        /// <param name="fixture"></param>
        public ComponentIntegrationTests(AccessoriesApiFixture fixture) : base(fixture)
        {


        }

        /// <summary>
        /// Получение запроса всех компонентов
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var componentItem = DataGeneratorRepository.Component();
            var componentItemDeleted = DataGeneratorRepository.Component(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Components.AddRangeAsync(componentItem, componentItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync("/Components");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<ComponentsResponse>>(resultString);
            result.Should().NotBeNull()
                .And.ContainSingle(x => x.Id == componentItem.Id)
                .And.NotContain(x => x.Id == componentItemDeleted.Id);
        }

        /// <summary>
        /// Получение запроса компонента по id
        /// </summary>
        [Fact]
        public async Task GetValueById()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var componentItem = DataGeneratorRepository.Component();
            var componentItemDeleted = DataGeneratorRepository.Component(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Components.AddRangeAsync(componentItem, componentItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync($"/Components/{componentItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ComponentsResponse>(resultString);
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    componentItem.Id,
                });
        }

        /// <summary>
        /// Добавление компонента
        /// </summary>
        [Fact]
        public async Task PostValueAdd()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var componentItem = DataGeneratorRepository.Component();
            var componentItemDeleted = DataGeneratorRepository.Component(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await context.Components.AddRangeAsync(componentItem, componentItemDeleted);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.CreateComponentRequest();

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PostAsync("/Components", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ComponentsResponse>(resultString);

            var componentResult = context.Components.Single(x => x.Id == result!.Id &&
                                                    x.Name == targetItem.Name);

            componentResult.Should().NotBeNull();
        }


        /// <summary>
        /// Изменения компонента
        /// </summary>
        [Fact]
        public async Task PutValueEdit()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var componentItem = DataGeneratorRepository.Component();
            var componentItemNew = DataGeneratorRepository.Component();
            await context.Components.AddRangeAsync(componentItem, componentItemNew);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.EditClientRequest();
            targetItem.Id = componentItem.Id;

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PutAsync($"/Components", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ComponentsResponse>(resultString);

            var componentResult = context.Components.Single(x => x.Id == targetItem.Id
                                                        && x.Name == targetItem.Name);

            componentResult.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление компонента
        /// </summary>
        [Fact]
        public async Task DeleteValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();
            var componentItem = DataGeneratorRepository.Component();
            var componentItemNew = DataGeneratorRepository.Component();
            await context.Components.AddRangeAsync(componentItem, componentItemNew);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.DeleteAsync($"/Components/{componentItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ClientsResponse>(resultString);

            var componentResult = context.Components.FirstOrDefault(x => x.Id == componentItem.Id);

            componentResult!.DeletedAt.Should().NotBeNull();
        }

    }
}
