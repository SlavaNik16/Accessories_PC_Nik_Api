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
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetService = DataGeneratorRepository.Service();
            await Context.Services.AddAsync(targetService);

            var target = DataGeneratorRepository.Order(x => { x.ClientId = targetClient.Id; x.ServiceId = targetService.Id; });
            await Context.Orders.AddRangeAsync(target,
                DataGeneratorRepository.Order(x =>
                {
                    x.ClientId = targetClient.Id;
                    x.ServiceId = targetService.Id;
                    x.DeletedAt = DateTimeOffset.UtcNow;
                }));

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
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetService = DataGeneratorRepository.Service();
            await Context.Services.AddAsync(targetService);

            var target = DataGeneratorRepository.Order(x => { x.ClientId = targetClient.Id; x.ServiceId = targetService.Id; });
            await Context.Orders.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            var entity = Context.Orders.Single(x => x.Id == target.Id &&
                x.ClientId == target.ClientId &&
                x.ServiceId == target.ServiceId);

            entity.DeletedAt.Should().BeNull();
        }

        // <summary>
        /// Добавление заказа, возвращает ошибку  - не добавлены покупки
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrowNotFoundWorker()
        {
            //Arrange
            var target = DataGeneratorService.OrderRequestModel();

            //Act
            Func<Task> act = () => orderService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesInvalidOperationException>()
                .WithMessage("Заказ без покупок недействителен! Нужно хотя бы выбрать компонент или услугу!");
        }

        // <summary>
        /// Добавление заказа, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnValue()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetService = DataGeneratorRepository.Service();
            await Context.Services.AddAsync(targetService);

            var targetComponent = DataGeneratorRepository.Component();
            await Context.Components.AddAsync(targetComponent);

            var targetDelivery = DataGeneratorRepository.Delivery();
            await Context.Deliveries.AddAsync(targetDelivery);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var target = DataGeneratorService.OrderRequestModel(x =>
            {
                x.ClientId = targetClient.Id;
                x.ServiceId = targetService.Id;
                x.ComponentId = targetComponent.Id;
                x.DeliveryId = targetDelivery.Id;
            });

            //Act
            var act = await orderService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Orders.Single(x =>
                x.Id == act.Id &&
                x.ClientId == target.ClientId &&
                x.ServiceId == target.ServiceId &&
                x.ComponentId == target.ComponentId &&
                x.DeliveryId == target.DeliveryId
            );
            entity.Should().NotBeNull();

        }

        // <summary>
        /// Изменение заказа, возвращает ошибку - заказ не найден
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnThrow()
        {
            //Arrange
            var targetModel = DataGeneratorService.OrderRequestModel();

            //Act
            Func<Task> act = () => orderService.EditAsync(targetModel, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Order>>()
                .WithMessage($"*{targetModel.Id}*");
        }


        /// <summary>
        /// Изменение заказа, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnValue()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetService = DataGeneratorRepository.Service();
            await Context.Services.AddAsync(targetService);

            var targetComponent = DataGeneratorRepository.Component();
            await Context.Components.AddAsync(targetComponent);

            var target = DataGeneratorRepository.Order(x => { x.ClientId = targetClient.Id; x.ServiceId = targetService.Id; });
            await Context.Orders.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = DataGeneratorService.OrderRequestModel();
            targetModel.Id = target.Id;
            targetModel.ClientId = target.ClientId;
            targetModel.ServiceId = null;
            targetModel.ComponentId = targetComponent.Id;
            //Act
            var act = await orderService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Orders.Single(x =>
                x.Id == act.Id &&
                x.ClientId == targetModel.ClientId &&
                x.ServiceId == null &&
                x.ComponentId == targetModel.ComponentId
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление заказа, возвращает ошибку - заказ не найден
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => orderService.DeleteAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Order>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление заказ, возвращает ошибку - заказ уже удален
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFountByDeleted()
        {
            //Arrange
            var targetClient = DataGeneratorRepository.Client();
            await Context.Clients.AddAsync(targetClient);

            var targetService = DataGeneratorRepository.Service();
            await Context.Services.AddAsync(targetService);

            var target = DataGeneratorRepository.Order(x => { x.ClientId = targetClient.Id; x.ServiceId = targetService.Id; x.DeletedAt = DateTimeOffset.UtcNow; });
            await Context.Orders.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => orderService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Order>>()
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

            var targetService = DataGeneratorRepository.Service();
            await Context.Services.AddAsync(targetService);

            var target = DataGeneratorRepository.Order(x => { x.ClientId = targetClient.Id; x.ServiceId = targetService.Id; });
            await Context.Orders.AddAsync(target);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => orderService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Orders.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}