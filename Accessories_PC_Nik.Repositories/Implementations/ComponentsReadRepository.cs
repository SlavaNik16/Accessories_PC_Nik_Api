using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ComponentsReadRepository : IComponentsReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public ComponentsReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<bool> IComponentsReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Component>()
                .NotDeletedAt()
                .AnyAsync(x=>x.Id == id , cancellationToken);

        Task<IReadOnlyCollection<Component>> IComponentsReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Component>()
                .NotDeletedAt()
                .OrderBy(x => x.MaterialType)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Component?> IComponentsReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Component>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Component>> IComponentsReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
           => reader.Read<Component>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.MaterialType)
                .ToDictionaryAsync(key => key.Id);

    }
}
