using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class WorkersReadRepository : IWorkersReadRepository
    {
        private readonly IAccessoriesContext context;

        public WorkersReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<List<Workers>> IWorkersReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Workers.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.AccessLevel)
                .ToList());

        Task<Workers?> IWorkersReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Workers.FirstOrDefault(x => x.Id == id));
    }
}
