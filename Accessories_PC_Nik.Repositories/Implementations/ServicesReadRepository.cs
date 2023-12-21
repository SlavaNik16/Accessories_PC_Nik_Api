using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ServicesReadRepository : IServicesReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public ServicesReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

       

        Task<IReadOnlyCollection<Service>> IServicesReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Services
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Service?> IServicesReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Services
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Service>> IServicesReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => context.Services
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id);
    }
}
