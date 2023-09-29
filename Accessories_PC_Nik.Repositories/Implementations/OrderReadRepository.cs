using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly IAccessoriesContext context;

        public OrderReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<List<Order>> IOrderReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Order.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Сlient_id)
                .ToList());

        Task<Order?> IOrderReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Order.FirstOrDefault(x => x.Id == id));

    }

}
