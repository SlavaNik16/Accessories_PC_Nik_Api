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
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Accessories_PC_Nik.Services.Tests.Tests
{
    public class DeliveryServiceTests : AccessoriesContextInMemory
    {
        private readonly IDeliveryService deliveryService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ServiceServiceTests"/>
        /// </summary>

        public DeliveryServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            deliveryService = new DeliveryService(
                new DeliveryReadRepository(Reader),
                new DeliveryWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение списка доставок и возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnNull()
        {
            // Act
            var result = await deliveryService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение списка доставок и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = DataGeneratorRepository.Delivery();
            await Context.Deliveries.AddRangeAsync(target, DataGeneratorRepository.Delivery(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await deliveryService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
              .NotBeNull()
              .And.HaveCount(1)
              .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение доставки по идентификатору возвращает ошибку
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnThrow()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => deliveryService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Delivery>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение доставки по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = DataGeneratorRepository.Delivery();
            await Context.Deliveries.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await deliveryService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.From,
                    target.To,
                    target.Price,
                });
        }

        // <summary>
        /// Добавление доставки, возвращает ошибку  - базы данных
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrow()
        {
            //Arrange
            var target = DataGeneratorService.DeliveryRequestModel(x => x.From = null);

            //Act
            Func<Task> act = () => deliveryService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>()
                .WithMessage($"*{target.From}*");
        }

        // <summary>
        /// Добавление доставки, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnValue()
        {
            //Arrange
            var target = DataGeneratorService.DeliveryRequestModel();

            //Act
            var act = await deliveryService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Deliveries.Single(x =>
                x.Id == act.Id &&
                x.From == target.From &&
                x.To == target.To &&
                x.Price == target.Price
            );
            entity.Should().NotBeNull();

        }
        // <summary>
        /// Изменение доставки, возвращает ошибку - доставка не найдена
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnThrow()
        {
            //Arrange
            var targetModel = DataGeneratorService.DeliveryRequestModel();

            //Act
            Func<Task> act = () => deliveryService.EditAsync(targetModel, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Delivery>>()
                .WithMessage($"*{targetModel.Id}*");
        }


        /// <summary>
        /// Изменение доставки, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnValue()
        {
            //Arrange
            var target = DataGeneratorRepository.Delivery();
            await Context.Deliveries.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = DataGeneratorService.DeliveryRequestModel(x => x.Id = target.Id);

            //Act
            var act = await deliveryService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Deliveries.Single(x =>
                x.Id == act.Id &&
                x.From == targetModel.From &&
                x.To == targetModel.To &&
                x.Price == targetModel.Price
            );
            entity.Should().NotBeNull();

        }
        /// <summary>
        /// Удаление доставки, возвращает ошибку - доставка не найдена
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => deliveryService.DeleteAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Delivery>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление доставки, возвращает ошибку - доставка уже удалена
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFountByDeleted()
        {
            //Arrange
            var target = DataGeneratorRepository.Delivery(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Deliveries.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => deliveryService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Delivery>>()
              .WithMessage($"*{target.Id}*");
        }

        /// <summary>
        /// Удаление доставки, возвращает - успешно
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnValue()
        {
            //Arrange
            var target = DataGeneratorRepository.Delivery();
            await Context.Deliveries.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => deliveryService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Deliveries.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
