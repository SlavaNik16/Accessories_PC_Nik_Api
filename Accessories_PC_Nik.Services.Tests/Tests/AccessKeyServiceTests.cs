using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Implementations;
using Accessories_PC_Nik.Services.Automappers;
using Accessories_PC_Nik.Services.Contracts.Exceptions;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Implementations;
using Accessories_PC_Nik.Tests.Generator;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Services.Tests.Tests
{
    public class AccessKeyServiceTests : AccessoriesContextInMemory
    {
        private readonly IAccessKeyService accessKeyService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AccessKeyServiceTests"/>
        /// </summary>

        public AccessKeyServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            accessKeyService = new AccessKeyService(
                new AccessKeyReadRepository(Reader),
                new AccessKeyWriteRepository(WriterContext),
                new WorkersReadRepository(Reader),
                new ClientsReadRepository(Reader),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение списка ключей и возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnNull()
        {

            // Act
            var result = await accessKeyService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();

        }

        /// <summary>
        /// Получение списка ключей и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = DataGeneratorRepository.Worker(x => x.ClientId = targetClient.Id);
            await Context.Workers.AddAsync(targetWorker);
            var target = DataGeneratorRepository.AccessKey(x => x.WorkerId = targetWorker.Id);

            await Context.AccessKeys.AddRangeAsync(target, DataGeneratorRepository.AccessKey(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await accessKeyService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
              .NotBeNull()
              .And.HaveCount(1)
              .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение ключа по идентификатору возвращает ошибку
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnThrow()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => accessKeyService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<AccessKey>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение ключа по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = DataGeneratorRepository.Worker(x => x.ClientId = targetClient.Id);
            await Context.Workers.AddAsync(targetWorker);

            var target = DataGeneratorRepository.AccessKey(x => x.WorkerId = targetWorker.Id);
            await Context.AccessKeys.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await accessKeyService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Key,
                    target.Types
                });
        }

        // <summary>
        /// Добавление ключа, возвращает ошибку  - не найден работник
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrowNotFoundWorker()
        {
            //Arrange
            var target = DataGeneratorService.AccessKeyRequestModel();

            //Act
            Func<Task> act = () => accessKeyService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Worker>>()
                .WithMessage($"*{target.WorkerId}*");
        }

        // <summary>
        /// Добавление ключа, возвращает ошибку  - работник не может создать ключ выше своего уровня
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrowInvalid()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = DataGeneratorRepository.Worker(x => { x.ClientId = targetClient.Id; x.AccessLevel = AccessLevelTypes.None; });
            await Context.Workers.AddAsync(targetWorker);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var target = DataGeneratorService.AccessKeyRequestModel(x => { x.WorkerId = targetWorker.Id; x.Types = AccessLevelTypes.Director; });

            //Act
            Func<Task> act = () => accessKeyService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesInvalidOperationException>()
                .WithMessage("Данный сотрудник не обладает правами, создавать ключ: c таким же уровнем или выше своего уровня доступа");

        }

        // <summary>
        /// Добавление ключа, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnValue()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = DataGeneratorRepository.Worker(x => { x.ClientId = targetClient.Id; x.AccessLevel = AccessLevelTypes.Director; });
            await Context.Workers.AddAsync(targetWorker);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var target = DataGeneratorService.AccessKeyRequestModel(x => x.WorkerId = targetWorker.Id);

            //Act
            var act = await accessKeyService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.AccessKeys.Single(x =>
                x.Id == act.Id &&
                x.Key == act.Key &&
                x.Types == target.Types &&
                x.WorkerId == targetWorker.Id
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление ключа, возвращает ошибку - ключ не найден
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => accessKeyService.DeleteAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<AccessKey>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление ключа, возвращает ошибку - клиент уже удален
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFountByDeleted()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = DataGeneratorRepository.Worker(x => x.ClientId = targetClient.Id);
            await Context.Workers.AddAsync(targetWorker);

            var target = DataGeneratorRepository.AccessKey(x => { x.WorkerId = targetWorker.Id; x.DeletedAt = DateTimeOffset.UtcNow; });
            await Context.AccessKeys.AddAsync(target);

            // Act
            Func<Task> act = () => accessKeyService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<AccessKey>>()
              .WithMessage($"*{target.Id}*");
        }

        /// <summary>
        /// Удаление клиента, возвращает - успешно
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnValue()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = DataGeneratorRepository.Worker(x => { x.ClientId = targetClient.Id; x.AccessLevel = AccessLevelTypes.Director; });
            await Context.Workers.AddAsync(targetWorker);

            var target = DataGeneratorRepository.AccessKey(x => x.WorkerId = targetWorker.Id);
            await Context.AccessKeys.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => accessKeyService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.AccessKeys.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
