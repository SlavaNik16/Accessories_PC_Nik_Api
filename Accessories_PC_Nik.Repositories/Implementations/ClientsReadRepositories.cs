using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ClientsReadRepositories : IClientsReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public ClientsReadRepositories(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<List<Clients>> IClientsReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Clients.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Clients?> IClientsReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Clients.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Clients>> IClientsReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
         => Task.FromResult(context.Clients.Where(x => x.DeleteAt == null && ids.Contains(x.Id))
             .OrderBy(x => x.Name)
             .ToDictionary(key => key.Id));
    }
}
