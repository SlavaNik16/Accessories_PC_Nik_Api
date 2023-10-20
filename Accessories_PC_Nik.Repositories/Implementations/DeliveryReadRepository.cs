using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class DeliveryReadRepository : IDeliveryReadRepository
    {
        private readonly IAccessoriesContext context;

        public DeliveryReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<List<Delivery>> IDeliveryReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Delivery.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.From)
                .ToList());

        Task<Delivery?> IDeliveryReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Delivery.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Delivery>> IDeliveryReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
           => Task.FromResult(context.Delivery.Where(x => x.DeleteAt == null && ids.Contains(x.Id))
               .OrderBy(x => x.From)
               .ToDictionary(key => key.Id));
    }
}
