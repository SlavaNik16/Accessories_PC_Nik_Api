using Accessories_PC_Nik.Api.Controllers;
using Accessories_PC_Nik.Api.Models;
using Accessories_PC_Nik.Api.Tests.Infrastructures;
using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Services.Contracts.Enums;
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
        /// Получение запроса всех ключей
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

            var accessKeyItem = DataGeneratorRepository.AccessKey(x=>x.WorkerId = workerItemAssistant.Id);
            var accessKeyItemDeleted = DataGeneratorRepository.AccessKey(x =>
            {
                x.DeletedAt = DateTimeOffset.UtcNow;
                x.WorkerId = workerItemAssistant.Id;
            });
            await context.AccessKeys.AddRangeAsync(accessKeyItem, accessKeyItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync("/AccessKey");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<AccessKeyResponse>>(resultString);
            result.Should().NotBeNull()
                .And.Contain(x => x.Id == accessKeyItem.Id)
                .And.NotContain(x => x.Id == accessKeyItemDeleted.Id);
        }

        /// <summary>
        /// Получение запроса ключа по id
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

            var accessKeyItem = DataGeneratorRepository.AccessKey(x => x.WorkerId = workerItemAssistant.Id);
            var accessKeyItemDeleted = DataGeneratorRepository.AccessKey(x =>
            {
                x.DeletedAt = DateTimeOffset.UtcNow;
                x.WorkerId = workerItemAssistant.Id;
            });
            await context.AccessKeys.AddRangeAsync(accessKeyItem, accessKeyItemDeleted);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.GetAsync($"/AccessKey/{accessKeyItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessKeyResponse>(resultString);
            result.Should().NotBeNull()
                .And.BeEquivalentTo(new
                {
                    accessKeyItem.Id,
                    accessKeyItem.Key,
                });
        }

        /// <summary>
        /// Добавление ключа
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
            var workerItemDirector = DataGeneratorRepository.Worker(x =>
            { 
                x.ClientId = clientItemNew.Id;
                x.AccessLevel = AccessLevelTypes.Director;
            });
            await context.Workers.AddRangeAsync(workerItemAssistant, workerItemDirector);

            var accessKeyItem = DataGeneratorRepository.AccessKey(x =>
            {
                x.WorkerId = workerItemAssistant.Id;
                x.Types = AccessLevelTypes.None;
            });

            var accessKeyItemNew = DataGeneratorRepository.AccessKey(x =>
            {
                x.WorkerId = workerItemDirector.Id;
                x.Types = AccessLevelTypes.DeputyDirector;
            });
            await context.AccessKeys.AddRangeAsync(accessKeyItem, accessKeyItemNew);
            await unitOfWork.SaveChangesAsync();

            var targetItem = DataGeneratorApi.CreateAccessKeyRequest();
            targetItem.WorkerId = workerItemDirector.Id;
            targetItem.Types = AccessLevelTypesModel.Seller;

            string data = JsonConvert.SerializeObject(targetItem);
            var contextData = new StringContent(data, Encoding.UTF8, "application/json");

            // Act
            var response = await clientHTTP.PostAsync($"/AccessKey", contextData);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessKeyResponse>(resultString);

            var workerResult = context.AccessKeys.Single(x => x.Id == result!.Id &&
                                                    x.Key == result.Key &&
                                                    x.WorkerId == targetItem.WorkerId);

            workerResult.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление ключа
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
                x.ClientId = clientItemNew.Id;
                x.AccessLevel = AccessLevelTypes.Director;
            });
            await context.Workers.AddRangeAsync(workerItemAssistant, workerItemDirector);

            var accessKeyItem = DataGeneratorRepository.AccessKey(x =>
            {
                x.WorkerId = workerItemAssistant.Id;
                x.Types = AccessLevelTypes.None;
            });

            var accessKeyItemNew = DataGeneratorRepository.AccessKey(x =>
            {
                x.WorkerId = workerItemDirector.Id;
                x.Types = AccessLevelTypes.DeputyDirector;
            });
            await context.AccessKeys.AddRangeAsync(accessKeyItem, accessKeyItemNew);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientHTTP.DeleteAsync($"/AccessKey/{accessKeyItem.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessKeyResponse>(resultString);

            var workerResult = context.AccessKeys.FirstOrDefault(x => x.Id == accessKeyItem.Id);

            workerResult!.DeletedAt.Should().NotBeNull();
        }
    }
}
