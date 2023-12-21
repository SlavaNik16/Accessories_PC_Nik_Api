using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class WorkersReadRepository : IWorkersReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public WorkersReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<IReadOnlyCollection<Worker>> IWorkersReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Workers
                .NotDeletedAt()
                .OrderBy(x => x.AccessLevel)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Worker?> IWorkersReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Workers
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
