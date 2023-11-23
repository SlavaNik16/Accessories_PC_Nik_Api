using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class AccessKeyReadRepository : IAccessKeyReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public AccessKeyReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<IReadOnlyCollection<AccessKey>> IAccessKeyReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.AccessKeys
                .NotDeletedAt()
                .OrderBy(x => x.Types)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<AccessKey?> IAccessKeyReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.AccessKeys
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

    }
}
