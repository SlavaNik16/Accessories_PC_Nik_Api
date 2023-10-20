using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class AccessKeyReadRepository : IAccessKeyReadRepository
    {
        private readonly IAccessoriesContext context;

        public AccessKeyReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<List<AccessKey>> IAccessKeyReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.AccessKey.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.Types)
                .ToList());

        Task<AccessKey?> IAccessKeyReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.AccessKey.FirstOrDefault(x => x.Id == id));

    }
}
