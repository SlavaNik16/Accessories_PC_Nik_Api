using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class OrderReadRepository : IOrderReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public OrderReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<IReadOnlyCollection<Order>> IOrderReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Orders
                .NotDeletedAt()
                .OrderBy(x => x.ClientId)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Order?> IOrderReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Orders
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

    }

}
