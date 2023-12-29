using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IComponentsReadRepository"/>
    /// </summary>
    public class ComponentReadRepositoryTests : AccessoriesContextInMemory
    {
        private readonly IComponentsReadRepository componentsReadRepository;

        public ComponentReadRepositoryTests()
        {
            componentsReadRepository = new ComponentsReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список компонентов
        /// </summary>
        [Fact]
        public async Task GetAllComponentEmpty()
        {
            // Act
            var result = await componentsReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список компонентов
        /// </summary>
        [Fact]
        public async Task GetAllComponentsValue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.Component();
            await Context.Components.AddRangeAsync(target,
                TestDataGeneratorRepository.Component(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await componentsReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение компонента по идентификатору и возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdComponentNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await componentsReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение компонента по идентификатору и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdComponentValue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.Component();
            await Context.Components.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await componentsReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка компонентов по идентификаторам и возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsComponentsEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await componentsReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка компонентов по идентификаторам и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsComponentsValue()
        {
            //Arrange
            var target1 = TestDataGeneratorRepository.Component();
            var target2 = TestDataGeneratorRepository.Component(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGeneratorRepository.Component();
            var target4 = TestDataGeneratorRepository.Component();
            await Context.Components.AddRangeAsync(target1, target2, target3, target4);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await componentsReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Получение ответа, существует ли такой компонент по Id, возвращает false
        /// </summary>
        [Fact]
        public async Task AnyByIdComponentFalse()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await componentsReadRepository.AnyByIdAsync(id, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Получение ответа, существует ли такой компонент по Id, возвращает true
        /// </summary>
        [Fact]
        public async Task AnyByIdComponentTrue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.Component();
            await Context.Components.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await componentsReadRepository.AnyByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }
    }
}