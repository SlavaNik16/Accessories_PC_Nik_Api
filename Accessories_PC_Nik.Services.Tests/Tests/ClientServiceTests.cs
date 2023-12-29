
using Accessories_PC_Nik.Context.Tests;
using Accessories_PC_Nik.Repositories.Implementations;
using Accessories_PC_Nik.Services.Automappers;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using Accessories_PC_Nik.Services.Implementations;
using AutoMapper;
using FluentAssertions;
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
        /// Получение персоны по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await clientService.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().Should().BeNull();
        }

        /// <summary>
        /// Получение персоны по идентификатору возвращает данные
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
            result.Should().Should()
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
        /// Добавление персоны, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
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

        /// <summary>
        /// Изменение персоны, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
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
        /// Удаление персоны, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
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
