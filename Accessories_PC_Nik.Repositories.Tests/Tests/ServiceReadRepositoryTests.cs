using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IServicesReadRepository"/>
    /// </summary>
    public class ServiceReadRepositoryTests : AccessoriesContextInMemory
    {
        private readonly IServicesReadRepository servicesReadRepository;

        public ServiceReadRepositoryTests()
        {
            servicesReadRepository = new ServicesReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список сервисов
        /// </summary>
        [Fact]
        public async Task GetAllServiceEmpty()
        {
            // Act
            var result = await servicesReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список сервисов
        /// </summary>
        [Fact]
        public async Task GetAllServicesValue()
        {
            //Arrange
            var target = TestDataGenerator.Service();
            await Context.Services.AddRangeAsync(target,
                TestDataGenerator.Service(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await servicesReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение сервиса по идентификатору и возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdServiceNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await servicesReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение сервиса по идентификатору и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdServiceValue()
        {
            //Arrange
            var target = TestDataGenerator.Service();
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await servicesReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка сервисов по идентификаторам и возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsServicesEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await servicesReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка сервисов по идентификаторам и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsServicesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Service();
            var target2 = TestDataGenerator.Service(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Service();
            var target4 = TestDataGenerator.Service();
            await Context.Services.AddRangeAsync(target1, target2, target3, target4);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await servicesReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Получение ответа, существует ли такой сервис по Id, возвращает false
        /// </summary>
        [Fact]
        public async Task AnyByIdServiceFalse()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await servicesReadRepository.AnyByIdAsync(id, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Получение ответа, существует ли такой сервис по Id, возвращает true
        /// </summary>
        [Fact]
        public async Task AnyByIdServiceTrue()
        {
            //Arrange
            var target = TestDataGenerator.Service();
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await servicesReadRepository.AnyByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Получение ответа, существует ли такое имя - не существует
        /// </summary>
        [Fact]
        public async Task AnyByNameServiceFalse()
        {
            //Arrange
            var target = TestDataGenerator.Service();

            // Act
            var result = await servicesReadRepository.AnyByNameAsync(target.Name, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Получение ответа, существует ли такое имя - существует
        /// </summary>
        [Fact]
        public async Task AnyByNameServiceTrue()
        {
            //Arrange
            var target = TestDataGenerator.Service();
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await servicesReadRepository.AnyByNameAsync(target.Name, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// Получить ответ, изменяем ли мы имя, совпадающее по такому id в бд - не изменяем
        /// </summary>
        [Fact]
        public async Task AnyByNameIsIdServiceFalse()
        {
            //Arrange
            var target = TestDataGenerator.Service();

            // Act
            var result = await servicesReadRepository.AnyByNameIsIdAsync(target.Name, target.Id, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// Получить ответ, изменяем ли мы имя, совпадающее по такому id в бд - изменяем
        /// </summary>
        [Fact]
        public async Task AnyByNameIsIdServiceTrue()
        {
            //Arrange
            var target = TestDataGenerator.Service();
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await servicesReadRepository.AnyByNameIsIdAsync(target.Name, target.Id, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }
    }
}
