using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ServicesReadRepository : IServicesReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public ServicesReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<bool> IServicesReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
          => reader.Read<Service>()
              .NotDeletedAt()
              .AnyAsync(x => x.Id == id, cancellationToken);

        Task<bool> IServicesReadRepository.AnyByNameAsync(string name, CancellationToken cancellationToken)
            => reader.Read<Service>()
                .NotDeletedAt()
                .AnyAsync(x => x.Name == name, cancellationToken);

        Task<IReadOnlyCollection<Service>> IServicesReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Service>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Service?> IServicesReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Service>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Service>> IServicesReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Service>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id);
    }
}
