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
    /// Тест для контроллера <see cref="AccessKeyController"/>
    /// </summary>
    public class AccessKeyIntegrationTests : BaseIntegrationTest
    {
        /// <summary>
        /// Инициализирую <see cref="AccessKeyIntegrationTests"/>
        /// </summary>
        /// <param name="fixture"></param>
        public AccessKeyIntegrationTests(AccessoriesApiFixture fixture) : base(fixture)
        {

        }

        /// <summary>
        /// Получение запроса всех работников
        /// </summary>
        [Fact]
        public async Task GetValue()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var workerItemAssistant = DataGeneratorRepository.Worker(x =>
            {
                x.ClientId = clientItem.Id;
                x.AccessLevel = AccessLevelTypes.Assistant;
            });
            var workerItemDeleted = DataGeneratorRepository.Worker(x =>
            {
                x.DeletedAt = DateTimeOffset.UtcNow;
                x.ClientId = clientItemNew.Id;
            });
            await context.Workers.AddRangeAsync(workerItemAssistant, workerItemDeleted);
            await unitOfWork.SaveChangesAsync();
            // Act
            var response = await clientHTTP.GetAsync("/Workers");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<WorkersResponse>>(resultString);
            result.Should().NotBeNull()
                .And.Contain(x => x.Id == workerItemAssistant.Id)
                .And.NotContain(x => x.Id == workerItemDeleted.Id);
        }

        /// <summary>
        /// Получение запроса работника по id
        /// </summary>
        [Fact]
        public async Task GetValueById()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var workerItemAssistant = DataGeneratorRepository.Worker(x =>
            {
                x.ClientId = clientItem.Id;
                x.AccessLevel = AccessLevelTypes.Assistant;
            });
            var workerItemDeleted = DataGeneratorRepository.Worker(x =>
            {
                x.DeletedAt = DateTimeOffset.UtcNow;
                x.ClientId = clientItemNew.Id;
            });
            await context.Workers.AddRangeAsync(workerItemAssistant, workerItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync($"/Workers/{workerItemAssistant.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WorkersResponse>(resultString);
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    workerItemAssistant.Id,
                });
        }

        /// <summary>
        /// Добавление работника
        /// </summary>
        [Fact]
        public async Task PostValueAdd()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var workerItemAssistant = DataGeneratorRepository.Worker(x =>
            {
                x.ClientId = clientItem.Id;
                x.AccessLevel = AccessLevelTypes.Assistant;
            });
            var workerItemDeleted = DataGeneratorRepository.Worker(x =>
            {
                x.DeletedAt = DateTimeOffset.UtcNow;
                x.ClientId = clientItemNew.Id;
            });
            await context.Workers.AddRangeAsync(workerItemAssistant, workerItemDeleted);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.CreateWorkerRequest();
            targetItem.ClientId = clientItemNew.Id;

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PostAsync($"/Workers", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WorkersResponse>(resultString);

            var workerResult = context.Workers.Single(x => x.Id == result!.Id &&
                                                    x.Number == targetItem.Number &&
                                                    x.AccessLevel == targetItem.AccessLevel &&
                                                    x.ClientId == targetItem.ClientId);

            workerResult.Should().NotBeNull();
        }

        /// <summary>
        /// Изменения работника(его прав) по ключу доступа
        /// </summary>
        [Fact]
        public async Task PutValueEditWithAccessKey()
        {
            //Arrange
            var clientHTTP = factory.CreateClient();

            var clientItem = DataGeneratorRepository.Client();
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var workerItemAssistant = DataGeneratorRepository.Worker(x =>
            {
                x.ClientId = clientItem.Id;
                x.AccessLevel = AccessLevelTypes.Assistant;
            });
            var workerItemDirector = DataGeneratorRepository.Worker(x =>
            {
                x.AccessLevel = AccessLevelTypes.Director;
                x.ClientId = clientItemNew.Id;
            });
            await context.Workers.AddRangeAsync(workerItemAssistant, workerItemDirector);

            var accessKeyItem = DataGeneratorRepository.AccessKey(x =>
            {
                x.Types = AccessLevelTypes.DeputyDirector;
                x.WorkerId = workerItemDirector.Id;
            });
            await context.AccessKeys.AddAsync(accessKeyItem);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.PutAsync($"/Workers/{workerItemAssistant.Id}/?key={accessKeyItem.Key}", null);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WorkersResponse>(resultString);

            var workerResult = context.Workers.FirstOrDefault(x => x.Id == workerItemAssistant.Id
                                                        && x.Number == workerItemAssistant.Number &&
                                                        x.ClientId == workerItemAssistant.ClientId &&
                                                        x.AccessLevel == accessKeyItem.Types);

            workerResult.Should().NotBeNull();
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
            var clientItemNew = DataGeneratorRepository.Client();
            await context.Clients.AddRangeAsync(clientItem, clientItemNew);

            var workerItemAssistant = DataGeneratorRepository.Worker(x =>
            {
                x.ClientId = clientItem.Id;
                x.AccessLevel = AccessLevelTypes.Assistant;
            });
            var workerItemDirector = DataGeneratorRepository.Worker(x =>
            {
                x.AccessLevel = AccessLevelTypes.Director;
                x.ClientId = clientItemNew.Id;
            });
            await context.Workers.AddRangeAsync(workerItemAssistant, workerItemDirector);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.DeleteAsync($"/Workers/{workerItemAssistant.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WorkersResponse>(resultString);

            var workerResult = context.Workers.FirstOrDefault(x => x.Id == workerItemAssistant.Id);

            workerResult!.DeletedAt.Should().NotBeNull();
        }
    }
}
