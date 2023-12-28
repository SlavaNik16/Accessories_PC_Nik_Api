using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Repositories.Tests.Tests
{
    /// <summary>
    /// “есты дл€ <see cref="IWorkersReadRepository"/>
    /// </summary>
    public class WorkerReadRepositoryTests : AccessoriesContextInMemory
    {
        private readonly IWorkersReadRepository workersReadRepository;

        public WorkerReadRepositoryTests()
        {
            workersReadRepository = new WorkersReadRepository(Reader);
        }

        /// <summary>
        /// ¬озвращает пустой список работников
        /// </summary>
        [Fact]
        public async Task GetAllWorkerEmpty()
        {
            // Act
            var result = await workersReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// ¬озвращает список работников
        /// </summary>
        [Fact]
        public async Task GetAllWorkersValue()
        {
            //Arrange
            var target = TestDataGenerator.Worker();
            await Context.Workers.AddRangeAsync(target,
                TestDataGenerator.Worker(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workersReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// ѕолучение работника по идентификатору и возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdWorkerNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await workersReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// ѕолучение работника по идентификатору и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdWorkerValue()
        {
            //Arrange
            var target = TestDataGenerator.Worker();
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workersReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// ѕолучение списка работников по идентификаторам и возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsClientsEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await workersReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// ѕолучение списка работников по идентификаторам и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsClientsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Worker();
            var target2 = TestDataGenerator.Worker(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Worker();
            var target4 = TestDataGenerator.Worker();
            await Context.Workers.AddRangeAsync(target1, target2, target3, target4);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workersReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// ѕолучение ответа, существует ли такой работник по Id, возвращает false
        /// </summary>
        [Fact]
        public async Task AnyByIdClientFalse()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await workersReadRepository.AnyByIdAsync(id, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// ѕолучение ответа, существует ли такой работник по Id, возвращает true
        /// </summary>
        [Fact]
        public async Task AnyByIdClientTrue()
        {
            //Arrange
            var target = TestDataGenerator.Worker();
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workersReadRepository.AnyByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// ѕолучение ответа, существует ли такой номер - не существует
        /// </summary>
        [Fact]
        public async Task AnyByNumberWorkerFalse()
        {
            //Arrange
            var target = TestDataGenerator.Worker();

            // Act
            var result = await workersReadRepository.AnyByNumberAsync(target.Number, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// ѕолучение ответа, существует ли такой номер - существует
        /// </summary>
        [Fact]
        public async Task AnyByNumberWorkerTrue()
        {
            //Arrange
            var target = TestDataGenerator.Worker();
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workersReadRepository.AnyByNumberAsync(target.Number, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }

        /// <summary>
        /// ѕолучить ответ, может ли данный работник по id, создать ключ доступа по такому типу - false
        /// </summary>
        [Fact]
        public async Task AnyByWorkerWithTypeFalse()
        {
            //Arrange
            var target = TestDataGenerator.Worker(x=>x.AccessLevel = AccessLevelTypes.Assistant);
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workersReadRepository.AnyByWorkerWithTypeAsync(target.Id, AccessLevelTypes.Assistant, CancellationToken);

            // Assert
            result.Should()
                .BeFalse();
        }

        /// <summary>
        /// ѕолучить ответ, может ли данный работник по id, создать ключ доступа по такому типу - true
        /// </summary>
        [Fact]
        public async Task AnyByWorkerWithTypeTrue()
        {
            //Arrange
            var target = TestDataGenerator.Worker(x => x.AccessLevel = AccessLevelTypes.Director);
            await Context.Workers.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await workersReadRepository.AnyByWorkerWithTypeAsync(target.Id, AccessLevelTypes.Assistant, CancellationToken);

            // Assert
            result.Should()
                .BeTrue();
        }
    }

}