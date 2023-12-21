using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class OrderReadRepository : IOrderReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public OrderReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Order>> IOrderReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Order>()
                .NotDeletedAt()
                .OrderBy(x => x.ClientId)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Order?> IOrderReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Order>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

    }

}
