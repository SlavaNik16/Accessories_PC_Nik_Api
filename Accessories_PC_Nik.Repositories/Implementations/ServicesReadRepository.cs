using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ServicesReadRepository : IServicesReadRepository, IReadRepositoryAnchor
    {
        private readonly IAccessoriesContext context;

        public ServicesReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

       

        Task<List<Services>> IServicesReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Services.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Services?> IServicesReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Services.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Services>> IServicesReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => Task.FromResult(context.Services.Where(x => x.DeleteAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionary(key => key.Id));
    }
}
