using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class WorkersService : IWorkersService, IServiceAnchor
    {
        private readonly IWorkersReadRepository workersReadRepository;
        private readonly IClientsReadRepository clientsReadRepository;
        private readonly IMapper mapper;

        public WorkersService(IWorkersReadRepository workersReadRepository,
            IClientsReadRepository clientsReadRepository,
            IMapper mapper)
        {
            this.workersReadRepository = workersReadRepository;
            this.clientsReadRepository = clientsReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<WorkersModel>> IWorkersService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await workersReadRepository.GetAllAsync(cancellationToken);

            var clients = await clientsReadRepository.GetByIdsAsync(result.Select(x => x.Client_id).Distinct(), cancellationToken);

            var listWorker = new List<WorkersModel>();
            foreach(var worker in result)
            {
                var work = mapper.Map<WorkersModel>(worker);
                clients.TryGetValue(worker.Client_id, out var client);
                work.ClientsModel = mapper.Map<ClientsModel>(client);
                listWorker.Add(work);
            }

            return listWorker;
        }

        async Task<WorkersModel?> IWorkersService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await workersReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;


            var client = await clientsReadRepository.GetByIdAsync(item.Client_id, cancellationToken);

            var work = mapper.Map<WorkersModel>(item);
            work.ClientsModel = client != null
                ? mapper.Map<ClientsModel>(client)
                : new ClientsModel();

            return work;
        }
    }
}
