using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IDeliveryReadRepository"/>
    /// </summary>
    public class DeliveryReadRepositoryTests : AccessoriesContextInMemory
    {
        private readonly IDeliveryReadRepository deliveriesReadRepository;

        public DeliveryReadRepositoryTests()
        {
            deliveriesReadRepository = new DeliveryReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список доставок
        /// </summary>
        [Fact]
        public async Task GetAllDeliveryEmpty()
        {
            // Act
            var result = await deliveriesReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список доставок
        /// </summary>
        [Fact]
        public async Task GetAllDeliveriesValue()
        {
            //Arrange
            var target = TestDataGenerator.Delivery();
            await Context.Deliveries.AddRangeAsync(target,
                TestDataGenerator.Delivery(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await deliveriesReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение доставки по идентификатору и возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdDeliveryNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await deliveriesReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение доставки по идентификатору и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdDeliveryValue()
        {
            //Arrange
            var target = TestDataGenerator.Delivery();
            await Context.Deliveries.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await deliveriesReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка доставок по идентификаторам и возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsDeliveriesEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await deliveriesReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка доставок по идентификаторам и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsDeliveriesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Delivery();
            var target2 = TestDataGenerator.Delivery(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Delivery();
            var target4 = TestDataGenerator.Delivery();
            await Context.Deliveries.AddRangeAsync(target1, target2, target3, target4);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await deliveriesReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Получение ответа, существует ли такая доставка по Id, возвращает false
        /// </summary>
        [Fact]
        public async Task AnyByIdDeliveryFalse()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await deliveriesReadRepository.AnyByIdAsync(id, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Получение ответа, существует ли такая доставка по Id, возвращает true
        /// </summary>
        [Fact]
        public async Task AnyByIdDeliveryTrue()
        {
            //Arrange
            var target = TestDataGenerator.Delivery();
            await Context.Deliveries.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await deliveriesReadRepository.AnyByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

       
    }
}
