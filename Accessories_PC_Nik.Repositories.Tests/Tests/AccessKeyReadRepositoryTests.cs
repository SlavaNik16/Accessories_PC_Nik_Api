using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Accessories_PC_Nik.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IAccessKeyReadRepository"/>
    /// </summary>
    public class AccessKeyReadRepositoryTests : AccessoriesContextInMemory
    {
        private readonly IAccessKeyReadRepository accessKeyReadRepository;

        public AccessKeyReadRepositoryTests()
        {
            accessKeyReadRepository = new AccessKeyReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список ключей доступа
        /// </summary>
        [Fact]
        public async Task GetAllAccessKeyEmpty()
        {
            // Act
            var result = await accessKeyReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список ключей доступа
        /// </summary>
        [Fact]
        public async Task GetAllAccessKeysValue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.AccessKey();
            await Context.AccessKeys.AddRangeAsync(target,
                TestDataGeneratorRepository.AccessKey(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await accessKeyReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение ключа доступа по идентификатору и возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdAccessKeyNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await accessKeyReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение ключа доступа по идентификатору и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdAccessKeyValue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.AccessKey();
            await Context.AccessKeys.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await accessKeyReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получить <see cref="AccessLevelTypes"/> по идентификатору ключа - отсутствие
        /// </summary>
        [Fact]
        public async Task GetAccessLevelByKeyEmpty()
        {
            //Arrange
            var target = TestDataGeneratorRepository.AccessKey();

            // Act
            var result = await accessKeyReadRepository.GetAccessLevelByKeyAsync(target.Key, CancellationToken);

            // Assert
            result.Should()
                .BeNull();
        }

        /// <summary>
        /// Получить <see cref="AccessLevelTypes"/> по идентификатору ключа - нахождение
        /// </summary>
        [Fact]
        public async Task GetAccessLevelByKeyValue()
        {
            //Arrange
            var target = TestDataGeneratorRepository.AccessKey();
            await Context.AccessKeys.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await accessKeyReadRepository.GetAccessLevelByKeyAsync(target.Key, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull();

        }
    }
}
