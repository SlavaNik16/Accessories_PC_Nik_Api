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

        async Task<IEnumerable<WorkerModel>> IWorkersService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await workersReadRepository.GetAllAsync(cancellationToken);

            var clients = await clientsReadRepository.GetByIdsAsync(result.Select(x => x.ClientId).Distinct(), cancellationToken);

            var listWorker = new List<WorkerModel>();
            foreach(var worker in result)
            {
                var work = mapper.Map<WorkerModel>(worker);
                if(!clients.TryGetValue(worker.ClientId, out var client))
                {
                    continue;
                }
                work.Clients = mapper.Map<Contracts.Models.ClientModel>(client);
                listWorker.Add(work);
            }
            return listWorker;
        }

        async Task<WorkerModel?> IWorkersService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await workersReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;


            var client = await clientsReadRepository.GetByIdAsync(item.ClientId, cancellationToken);

            var work = mapper.Map<WorkerModel>(item);
            work.Clients  = mapper.Map<Contracts.Models.ClientModel>(client);

            return work;
        }
    }
}
