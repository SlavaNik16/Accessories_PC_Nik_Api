
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
    public class ClientServiceTests : AccessoriesContextInMemory
    {
        private readonly IClientsService clientService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientServiceTests"/>
        /// </summary>

        public ClientServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            clientService = new ClientsService(
                new ClientsReadRepository(Reader),
                new ClientsWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение списка клиентов и возвращает пустой список
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnNull()
        {

            // Act
            var result = await clientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should().BeEmpty();

        }

        /// <summary>
        /// Получение списка клиентов и возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Client();
            await Context.Clients.AddRangeAsync(target, TestDataGeneratorService.Client(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await clientService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
              .NotBeNull()
              .And.HaveCount(1)
              .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение клиента по идентификатору возвращает ошибку
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnThrow()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => clientService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Client>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение клиента по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            var result = await clientService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Surname,
                    target.Name,
                    target.Phone,
                    target.Email
                });
        }

        // <summary>
        /// Добавление клиента, возвращает ошибку  - базы данных
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnThrow()
        {
            //Arrange
            var target = TestDataGeneratorService.ClientRequestModel(x => x.Name = null);

            //Act
            Func<Task> act = () => clientService.AddAsync(target, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<DbUpdateException>()
                .WithMessage($"*{target.Name}*");
        }

        // <summary>
        /// Добавление клиента, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.ClientRequestModel();

            //Act
            var act = await clientService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Clients.Single(x =>
                x.Id == act.Id &&
                x.Surname == target.Surname
            );
            entity.Should().NotBeNull();

        }
        // <summary>
        /// Изменение клиента, возвращает ошибку - клиент не найден
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnThrow()
        {
            //Arrange
            var targetModel = TestDataGeneratorService.ClientRequestModel();

            //Act
            Func<Task> act = () => clientService.EditAsync(targetModel, CancellationToken);

            //Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Client>>()
                .WithMessage($"*{targetModel.Id}*");
        }


        /// <summary>
        /// Изменение клиента, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGeneratorService.ClientRequestModel();
            targetModel.Id = target.Id;
            targetModel.Patronymic = null;
            //Act
            var act = await clientService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Clients.Single(x =>
                x.Id == act.Id &&
                x.Surname == targetModel.Surname &&
                x.Patronymic == null
            );
            entity.Should().NotBeNull();

        }
        /// <summary>
        /// Удаление клиента, возвращает ошибку - клиент не найден
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => clientService.DeleteAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Client>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление клиента, возвращает ошибку - клиент уже удален
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnThrowNotFountByDeleted()
        {
            //Arrange
            var target = TestDataGeneratorService.Client(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Clients.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => clientService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<AccessoriesEntityNotFoundException<Client>>()
              .WithMessage($"*{target.Id}*");
        }

        /// <summary>
        /// Удаление клиента, возвращает - успешно
        /// </summary>
        [Fact]
        public async Task DeleteShouldWorkReturnValue()
        {
            //Arrange
            var target = TestDataGeneratorService.Client();
            await Context.Clients.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => clientService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Clients.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

    }
}
