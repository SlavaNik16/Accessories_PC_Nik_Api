using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.ModelRequest;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;
using System.Reflection.Metadata;
using TimeTable203.Services.Contracts.Exceptions;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ClientsService : IClientsService, IServiceAnchor
    {
        private readonly IClientsReadRepository clientsReadRepository;
        private readonly IClientsWriteRepository clientsWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ClientsService(IClientsReadRepository clientsReadRepository,
            IClientsWriteRepository clientsWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.clientsReadRepository = clientsReadRepository;
            this.clientsWriteRepository = clientsWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

       
        async Task<IEnumerable<ClientModel>> IClientsService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientsReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ClientModel>>(result);
        }

        async Task<ClientModel?> IClientsService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsReadRepository.GetByIdAsync(id, cancellationToken);
            if(item == null) return null;

            return mapper.Map<ClientModel>(item);
        } 
        
        async Task<ClientModel> IClientsService.AddAsync(ClientRequestModel source, CancellationToken cancellationToken)
        {
            var item = new Client
            {
                Id = Guid.NewGuid(),
                Surname = source.Surname,
                Name = source.Name,
                Patronymic = source.Patronymic,
                Phone = source.Phone,
                Email = source.Email,
                Balance = source.Balance,
            };
            clientsWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ClientModel>(item);
        }

        

        async Task<ClientModel> IClientsService.EditAsync(ClientRequestModel source, CancellationToken cancellationToken)
        {
            var item = new Client();
            clientsWriteRepository.Update(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ClientModel>(item);
        }
        async Task IClientsService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetAccessKey = await clientsReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetAccessKey == null)
            {
                throw new AccessoriesEntityNotFoundException<Client>(id);
            }
            if (targetAccessKey.DeletedAt.HasValue)
            {
                throw new AccessoriesInvalidOperationException($"Клиент с идентификатором {id} уже удален");
            }

            clientsWriteRepository.Delete(targetAccessKey);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
