using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Context.Tests;
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
    public class ComponentServiceTests : AccessoriesContextInMemory
    {
        private readonly IComponentsService componentService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientServiceTests"/>
        /// </summary>

        public ComponentServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            componentService = new ComponentsService(
                new ComponentsReadRepository(Reader),
                new ComponentsWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение списка компонентов и возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnNull()
        {

            // Act
            var result = await componentService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();

        }

        /// <summary>
        /// Получение списка компонентов и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Component();
            await Context.Components.AddRangeAsync(target, TestDataGeneratorService.Component(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await componentService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
              .NotBeNull()
              .And.HaveCount(1)
              .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение компонента по идентификатору возвращает ошибку
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnThrow()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => componentService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Component>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение компонента по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Component();
            await Context.Components.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await componentService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Price,
                    target.Count
                });
        }

        // <summary>
        /// Добавление компонента, возвращает ошибку  - базы данных
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrow()
        {
            //Arrange
            var target = TestDataGeneratorService.ComponentRequestModel(x => x.Name = null);

            //Act
            Func<Task> act = () => componentService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>()
                .WithMessage($"*{target.Name}*");
        }

        // <summary>
        /// Добавление компонента, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.ComponentRequestModel();

            //Act
            var act = await componentService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Components.Single(x =>
                x.Id == act.Id &&
                x.Name == target.Name &&
                x.Price == target.Price &&
                x.Count == target.Count
            );
            entity.Should().NotBeNull();

        }
        // <summary>
        /// Изменение компонента, возвращает ошибку - компонент не найден
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnThrow()
        {
            //Arrange
            var targetModel = TestDataGeneratorService.ComponentRequestModel();

            //Act
            Func<Task> act = () => componentService.EditAsync(targetModel, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Component>>()
                .WithMessage($"*{targetModel.Id}*");
        }


        /// <summary>
        /// Изменение компонент, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Component();
            await Context.Components.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGeneratorService.ComponentRequestModel(x => x.Id = target.Id);

            //Act
            var act = await componentService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Components.Single(x =>
                  x.Id == act.Id &&
                x.Name == targetModel.Name &&
                x.Price == targetModel.Price &&
                x.Count == targetModel.Count
            );
            entity.Should().NotBeNull();

        }
        /// <summary>
        /// Удаление компонента, возвращает ошибку - компонент не найден
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => componentService.DeleteAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Component>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление компонента, возвращает ошибку - компонент уже удален
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFountByDeleted()
        {
            //Arrange
            var target = TestDataGeneratorService.Component(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Components.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => componentService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Component>>()
              .WithMessage($"*{target.Id}*");
        }

        /// <summary>
        /// Удаление компонента, возвращает - успешно
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Component();
            await Context.Components.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => componentService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Components.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
