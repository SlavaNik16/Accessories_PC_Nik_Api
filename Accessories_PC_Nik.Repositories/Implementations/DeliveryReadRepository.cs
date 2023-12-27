using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class DeliveryReadRepository : IDeliveryReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public DeliveryReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }
        Task<bool> IDeliveryReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
         => reader.Read<Delivery>()
             .NotDeletedAt()
             .AnyAsync(x => x.Id == id, cancellationToken);

        Task<IReadOnlyCollection<Delivery>> IDeliveryReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Delivery>()
                .NotDeletedAt()
                .OrderBy(x => x.From)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Delivery?> IDeliveryReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Delivery>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Delivery>> IDeliveryReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
           => reader.Read<Delivery>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.From)
                .ToDictionaryAsync(key => key.Id, cancellationToken);
    }
}
