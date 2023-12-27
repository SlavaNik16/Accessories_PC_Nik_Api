using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class WorkersReadRepository : IWorkersReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public WorkersReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<bool> IWorkersReadRepository.AnyByNumberAsync(string number, CancellationToken cancellationToken)
            => reader.Read<Worker>()
                .NotDeletedAt()
                .AnyAsync(x => x.Number == number, cancellationToken);

        Task<IReadOnlyCollection<Worker>> IWorkersReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Worker>()
                .NotDeletedAt()
                .OrderBy(x => x.AccessLevel)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Worker?> IWorkersReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Worker>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
