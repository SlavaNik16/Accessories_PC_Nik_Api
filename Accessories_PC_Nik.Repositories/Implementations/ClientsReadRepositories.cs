using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ClientsReadRepositories : IClientsReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public ClientsReadRepositories(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<IReadOnlyCollection<Client>> IClientsReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Clients
                .NotDeletedAt()
                .OrderBy(x => x.Surname)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Client?> IClientsReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Clients
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Client>> IClientsReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => context.Clients
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id);
    }
}
