using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using Accessories_PC_Nik.Services.Automappers;
using Accessories_PC_Nik.Services.Contracts.Exceptions;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Implementations;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Accessories_PC_Nik.Services.Tests.Tests
{
    public class WorkerServiceTests : AccessoriesContextInMemory
    {
        private readonly IWorkersService workerService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="WorkerServiceTests"/>
        /// </summary>

        public WorkerServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            workerService = new WorkersService(
                new WorkersReadRepository(Reader),
                new WorkersWriteRepository(WriterContext),
                new ClientsReadRepository(Reader),
                new AccessKeyReadRepository(Reader),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение списка работников и возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnNull()
        {

            // Act
            var result = await workerService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();

        }

        /// <summary>
        /// Получение списка работников и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
          

            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);

            var target = TestDataGeneratorService.Worker(x=> x.ClientId = targetClient.Id);

            await Context.Workers.AddRangeAsync(target, 
                TestDataGeneratorService.Worker(x => { x.ClientId = targetClient.Id; x.DeletedAt = DateTimeOffset.UtcNow; }));

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workerService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
              .NotBeNull()
              .And.HaveCount(1)
              .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает ошибку
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnThrow()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => workerService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Worker>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение работника по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker();
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workerService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Number,
                    target.Series,
                    target.IssuedBy,
                    target.IssuedAt
                });
        }

        // <summary>
        /// Добавление работника, возвращает ошибку - базы данных
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrow()
        {
            //Arrange
            var target = TestDataGeneratorService.WorkerRequestModel(x => x.Number = null);

            //Act
            Func<Task> act = () => workerService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>()
                .WithMessage($"*{target.Number}*");
        }

        // <summary>
        /// Добавление работника, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.WorkerRequestModel();

            //Act
            var act = await workerService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Workers.Single(x =>
                x.Id == act.Id &&
                x.Number == target.Number &&
                x.Series == target.Series
            );
            entity.Should().NotBeNull();

        }
        // <summary>
        /// Изменение работника, возвращает ошибку  - работник не найден
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnThrow()
        {
            //Arrange
            var targetModel = TestDataGeneratorService.WorkerRequestModel();

            //Act
            Func<Task> act = () => workerService.EditAsync(targetModel, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Worker>>()
                .WithMessage($"*{targetModel.Id}*");
        }


        /// <summary>
        /// Изменение работника, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker();
            await Context.Workers.AddAsync(target);

            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGeneratorService.WorkerRequestModel();
            targetModel.Id = target.Id;
            targetModel.Number = target.Number;
            targetModel.AccessLevel = AccessLevelTypes.Director;
            targetModel.ClientId = targetClient.Id;
            //Act
            var act = await workerService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Workers.Single(x =>
                x.Id == act.Id &&
                x.Number == targetModel.Number &&
                x.AccessLevel == targetModel.AccessLevel &&
                x.ClientId == targetModel.ClientId
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение работника, применяя ключ доступа, вызывает ошибку - не найден работник
        /// </summary>
        [Fact]
        public async Task EditAccessKeyShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker();
            await Context.Workers.AddAsync(target);

            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetAccessKey = TestDataGeneratorService.AccessKey(x => { x.WorkerId = target.Id; x.Types = AccessLevelTypes.DeputyDirector; });
            await Context.AccessKeys.AddAsync(targetAccessKey);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var id = Guid.NewGuid();

            //Act
            Func<Task> act = () => workerService.EditWithAccessKeyAsync(id, targetAccessKey.Key, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Worker>>()
               .WithMessage($"*{id}*");

        }

        /// <summary>
        /// Изменение работника, применяя ключ доступа, вызывает ошибку - не найден ключ
        /// </summary>
        [Fact]
        public async Task EditAccessKeyShouldWorkReturnThrowInvalidKey()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker();
            await Context.Workers.AddAsync(target);

            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetAccessKey = TestDataGeneratorService.AccessKey(x => x.WorkerId = target.Id);
            await Context.AccessKeys.AddAsync(targetAccessKey);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var key = Guid.NewGuid();

            //Act
            Func<Task> act = () => workerService.EditWithAccessKeyAsync(target.Id, key, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesInvalidOperationException>()
               .WithMessage("Такого ключа нет в наличии!");

        }

        /// <summary>
        /// Изменение работника, применяя ключ доступа, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditAccessKeyShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker();
            await Context.Workers.AddAsync(target);

            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetAccessKey = TestDataGeneratorService.AccessKey(x=> x.WorkerId = target.Id);
            await Context.AccessKeys.AddAsync(targetAccessKey);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            var act = await workerService.EditWithAccessKeyAsync(target.Id, targetAccessKey.Key, CancellationToken);

            //Assert

            var entity = Context.Workers.Single(x =>
                x.Id == act.Id &&
                x.Number == target.Number &&
                x.AccessLevel == targetAccessKey.Types &&
                x.ClientId == target.ClientId
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление работника, возвращает ошибку - работник не найден
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker();
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => workerService.DeleteAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Worker>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление работника, возвращает ошибку - работник уже удален
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFountByDeleted()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => workerService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Worker>>()
              .WithMessage($"*{target.Id}*");
        }

        /// <summary>
        /// Удаление работника, возвращает - успешно
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Worker();
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => workerService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Workers.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}
