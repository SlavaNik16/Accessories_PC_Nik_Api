using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class WorkersService : IWorkersService
    {
        private readonly IWorkersReadRepository workersReadRepository;

        public WorkersService(IWorkersReadRepository workersReadRepository)
        {
            this.workersReadRepository = workersReadRepository;
        }

        async Task<IEnumerable<WorkersModel>> IWorkersService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await workersReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new WorkersModel
            {
                Id = x.Id,
                Number = x.Number,
                Series = x.Series,
                IssuedAt = x.IssuedAt,
                IssuedBy = x.IssuedBy,
                DocumentType = x.DocumentType,
                AccessLevel = x.AccessLevel,
            });
        }

        async Task<WorkersModel?> IWorkersService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await workersReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new WorkersModel
            {
                Id = item.Id,
                Number = item.Number,
                Series = item.Series,
                IssuedAt = item.IssuedAt,
                IssuedBy = item.IssuedBy,
                DocumentType = item.DocumentType,
                AccessLevel = item.AccessLevel,
            };
        }
    }
}
