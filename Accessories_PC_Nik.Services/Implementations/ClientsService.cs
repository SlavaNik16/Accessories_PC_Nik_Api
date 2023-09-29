using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsReadRepository clientsReadRepository;
        public ClientsService(IClientsReadRepository clientsReadRepository)
        {
            this.clientsReadRepository = clientsReadRepository;
        }
        async Task<IEnumerable<ClientsModel>> IClientsService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientsReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new ClientsModel
            {
                Id = x.Id,
                Surname = x.Surname,
                Name = x.Name,
                Patronymic = x.Patronymic,
                Phone = x.Phone,
                Email = x.Email,
                Balance = x.Balance,
            });
        }

        async Task<ClientsModel?> IClientsService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsReadRepository.GetByIdAsync(id, cancellationToken);
            if(item == null) return null;

            return new ClientsModel
            {
                Id = item.Id,
                Surname = item.Surname,
                Name = item.Name,
                Patronymic = item.Patronymic,
                Phone = item.Phone,
                Email = item.Email,
                Balance = item.Balance,
            };
        }
    }
}
