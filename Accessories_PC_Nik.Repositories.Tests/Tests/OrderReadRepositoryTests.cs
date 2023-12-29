using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IOrderReadRepository"/>
    /// </summary>
    public class OrderReadRepositoryTests : AccessoriesContextInMemory
    {
        private readonly IOrderReadRepository orderReadRepository;

        public OrderReadRepositoryTests()
        {
            orderReadRepository = new OrderReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список заказов
        /// </summary>
        [Fact]
        public async Task GetAllOrderEmpty()
        {
            // Act
            var result = await orderReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список заказов
        /// </summary>
        [Fact]
        public async Task GetAllOrdersValue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.Order();
            await Context.Orders.AddRangeAsync(target,
                TestDataGeneratorRepository.Order(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение заказа по идентификатору и возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdOrderNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await orderReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение заказа по идентификатору и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdOrderValue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.Order();
            await Context.Orders.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await orderReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

    }
}
