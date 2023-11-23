using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ComponentsReadRepository : IComponentsReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public ComponentsReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<IReadOnlyCollection<Component>> IComponentsReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Components
                .NotDeletedAt()
                .OrderBy(x => x.MaterialType)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Component?> IComponentsReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Components
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Component>> IComponentsReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
           => context.Components
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.MaterialType)
                .ToDictionaryAsync(key => key.Id);

    }
}
