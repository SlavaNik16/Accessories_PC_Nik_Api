using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Enums;
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

        Task<bool> IWorkersReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Worker>()
                .NotDeletedAt()
                .AnyAsync(x => x.Id == id, cancellationToken);

        Task<bool> IWorkersReadRepository.AnyByNumberAsync(string number, CancellationToken cancellationToken)
            => reader.Read<Worker>()
                .NotDeletedAt()
                .AnyAsync(x => x.Number == number, cancellationToken);

        Task<bool> IWorkersReadRepository.AnyByWorkerWithTypeAsync(Guid id, AccessLevelTypes accessLevelTypes, CancellationToken cancellationToken)
            => reader.Read<Worker>()
                .ById(id)
                .AnyAsync(x => x.AccessLevel > accessLevelTypes, cancellationToken);

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

        Task<Dictionary<Guid, Worker>> IWorkersReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
         => reader.Read<Worker>()
             .NotDeletedAt()
             .ByIds(ids)
             .OrderBy(x => x.CreatedAt)
             .ToDictionaryAsync(key => key.Id, cancellationToken);
    }
}
