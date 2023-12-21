using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class DeliveryReadRepository : IDeliveryReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public DeliveryReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<IReadOnlyCollection<Delivery>> IDeliveryReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Deliveries
                .NotDeletedAt()
                .OrderBy(x => x.From)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Delivery?> IDeliveryReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Deliveries
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Delivery>> IDeliveryReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
           => context.Deliveries
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.From)
                .ToDictionaryAsync(key => key.Id);
    }
}
