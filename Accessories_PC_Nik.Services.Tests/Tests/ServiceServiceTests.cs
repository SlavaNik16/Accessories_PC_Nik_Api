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
    public class ServiceServiceTests : AccessoriesContextInMemory
    {
        private readonly IServicesService serviceService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ServiceServiceTests"/>
        /// </summary>

        public ServiceServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            serviceService = new ServicesService(
                new ServicesReadRepository(Reader),
                new ServicesWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение списка услуг и возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnNull()
        {
            // Act
            var result = await serviceService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Получение списка услуг и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Service();
            await Context.Services.AddRangeAsync(target, TestDataGeneratorService.Service(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await serviceService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
              .NotBeNull()
              .And.HaveCount(1)
              .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение услуги по идентификатору возвращает ошибку
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnThrow()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => serviceService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Service>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение услуги по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Service();
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await serviceService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Duration,
                    target.Price,
                });
        }

        // <summary>
        /// Добавление услуги, возвращает ошибку  - базы данных
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrow()
        {
            //Arrange
            var target = TestDataGeneratorService.ServiceRequestModel(x => x.Name = null);

            //Act
            Func<Task> act = () => serviceService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>()
                .WithMessage($"*{target.Name}*");
        }

        // <summary>
        /// Добавление услуги, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.ServiceRequestModel();

            //Act
            var act = await serviceService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Services.Single(x =>
                x.Id == act.Id &&
                x.Name == target.Name &&
                x.Duration == target.Duration &&
                x.Price == target.Price
            );
            entity.Should().NotBeNull();

        }
        // <summary>
        /// Изменение услуги, возвращает ошибку - услуга не найдена
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnThrow()
        {
            //Arrange
            var targetModel = TestDataGeneratorService.ServiceRequestModel();

            //Act
            Func<Task> act = () => serviceService.EditAsync(targetModel, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Service>>()
                .WithMessage($"*{targetModel.Id}*");
        }


        /// <summary>
        /// Изменение услуги, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Service();
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGeneratorService.ServiceRequestModel(x => x.Id = target.Id);

            //Act
            var act = await serviceService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Services.Single(x =>
                x.Id == act.Id &&
                x.Name == targetModel.Name &&
                x.Duration == targetModel.Duration &&
                x.Price == targetModel.Price
            );
            entity.Should().NotBeNull();

        }
        /// <summary>
        /// Удаление услуги, возвращает ошибку - услуга не найдена
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => serviceService.DeleteAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Service>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление услуги, возвращает ошибку - услуга уже удалена
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFountByDeleted()
        {
            //Arrange
            var target = TestDataGeneratorService.Service(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => serviceService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Service>>()
              .WithMessage($"*{target.Id}*");
        }

        /// <summary>
        /// Удаление услуги, возвращает - успешно
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Service();
            await Context.Services.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => serviceService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Services.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
