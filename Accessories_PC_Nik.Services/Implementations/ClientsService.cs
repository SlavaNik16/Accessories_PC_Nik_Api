using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ClientsService : IClientsService
    {
        private readonly IClientsReadRepository clientsReadRepository;
        private readonly IMapper mapper;
        public ClientsService(IClientsReadRepository clientsReadRepository,
            IMapper mapper)
        {
            this.clientsReadRepository = clientsReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<ClientsModel>> IClientsService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientsReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ClientsModel>>(result);
        }

        async Task<ClientsModel?> IClientsService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsReadRepository.GetByIdAsync(id, cancellationToken);
            if(item == null) return null;

            return mapper.Map<ClientsModel>(item);
        }
    }
}
