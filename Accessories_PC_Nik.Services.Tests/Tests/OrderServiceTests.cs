using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Implementations;
using Accessories_PC_Nik.Services.Automappers;
using Accessories_PC_Nik.Services.Contracts.Exceptions;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Implementations;
using AutoMapper;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Services.Tests.Tests
{
    public class OrderServiceTests : AccessoriesContextInMemory
    {
        private readonly IOrderService orderService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="OrderServiceTests"/>
        /// </summary>

        public OrderServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            orderService = new OrderService(
                new OrderReadRepository(Reader),
                new ServicesReadRepository(Reader),
                new ComponentsReadRepository(Reader),
                new DeliveryReadRepository(Reader),
                new ClientsReadRepository(Reader),
                new OrderWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение списка заказов и возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnNull()
        {

            // Act
            var result = await orderService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();

        }

        /// <summary>
        /// Получение списка заказов и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetService = TestDataGeneratorService.Service();
            await Context.Services.AddAsync(targetService);

            var target = TestDataGeneratorService.Order(x=> { x.ClientId = targetClient.Id; x.ServiceId = targetService.Id; });
            await Context.Orders.AddRangeAsync(target,
                TestDataGeneratorService.Order(x => { x.ClientId = targetClient.Id;
                                                    x.ServiceId = targetService.Id; 
                                                    x.DeletedAt = DateTimeOffset.UtcNow; }));

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
              .NotBeNull()
              .And.HaveCount(1)
              .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение заказа по идентификатору возвращает ошибку
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnThrow()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => orderService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Order>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение заказа по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetService = TestDataGeneratorService.Service();
            await Context.Services.AddAsync(targetService);

            var target = TestDataGeneratorService.Order(x => { x.ClientId = targetClient.Id; x.ServiceId = targetService.Id; });
            await Context.Orders.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderService.GetByIdAsync(target.Id, CancellationToken);

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
            var target = TestDataGeneratorService.AccessKeyRequestModel();

            //Act
            Func<Task> act = () => orderService.AddAsync(target, CancellationToken);

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
            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = TestDataGeneratorService.Worker(x => { x.ClientId = targetClient.Id; x.AccessLevel = AccessLevelTypes.None; });
            await Context.Workers.AddAsync(targetWorker);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var target = TestDataGeneratorService.AccessKeyRequestModel(x => { x.WorkerId = targetWorker.Id; x.Types = AccessLevelTypes.Director; });

            //Act
            Func<Task> act = () => orderService.AddAsync(target, CancellationToken);

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
            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = TestDataGeneratorService.Worker(x => { x.ClientId = targetClient.Id; x.AccessLevel = AccessLevelTypes.Director; });
            await Context.Workers.AddAsync(targetWorker);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var target = TestDataGeneratorService.AccessKeyRequestModel(x => x.WorkerId = targetWorker.Id);

            //Act
            var act = await orderService.AddAsync(target, CancellationToken);

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
            Func<Task> act = () => orderService.DeleteAsync(id, CancellationToken);

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
            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = TestDataGeneratorService.Worker(x => x.ClientId = targetClient.Id);
            await Context.Workers.AddAsync(targetWorker);

            var target = TestDataGeneratorService.AccessKey(x => { x.WorkerId = targetWorker.Id; x.DeletedAt = DateTimeOffset.UtcNow; });
            await Context.AccessKeys.AddAsync(target);

            // Act
            Func<Task> act = () => orderService.DeleteAsync(target.Id, CancellationToken);

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
            var targetClient = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(targetClient);
            var targetWorker = TestDataGeneratorService.Worker(x => { x.ClientId = targetClient.Id; x.AccessLevel = AccessLevelTypes.Director; });
            await Context.Workers.AddAsync(targetWorker);

            var target = TestDataGeneratorService.AccessKey(x => x.WorkerId = targetWorker.Id);
            await Context.AccessKeys.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => orderService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.AccessKeys.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}