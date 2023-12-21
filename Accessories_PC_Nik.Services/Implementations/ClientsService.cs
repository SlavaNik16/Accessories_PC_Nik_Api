using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class ClientsService : IClientsService, IServiceAnchor
    {
        private readonly IClientsReadRepository clientsReadRepository;
        private readonly IMapper mapper;
        public ClientsService(IClientsReadRepository clientsReadRepository,
            IMapper mapper)
        {
            this.clientsReadRepository = clientsReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<Contracts.Models.ClientModel>> IClientsService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientsReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<Contracts.Models.ClientModel>>(result);
        }

        async Task<Contracts.Models.ClientModel?> IClientsService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientsReadRepository.GetByIdAsync(id, cancellationToken);
            if(item == null) return null;

            return mapper.Map<Contracts.Models.ClientModel>(item);
        }
    }
}
