using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ClientsReadRepository : IClientsReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public ClientsReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<bool> IClientsReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Client>()
                .NotDeletedAt()
                .AnyAsync(x => x.Id == id);

        Task<bool> IClientsReadRepository.AnyByPhoneAsync(string phone, CancellationToken cancellationToken)
          => reader.Read<Client>()
              .NotDeletedAt()
              .AnyAsync(x => x.Phone == phone);

        Task<bool> IClientsReadRepository.AnyByEmailAsync(string email, CancellationToken cancellationToken)
          => reader.Read<Client>()
              .NotDeletedAt()
              .AnyAsync(x => x.Email == email);

        Task<IReadOnlyCollection<Client>> IClientsReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Client>()
                .NotDeletedAt()
                .OrderBy(x => x.Surname)
                .ThenBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Client?> IClientsReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Client>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Client>> IClientsReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Client>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id);
    }
}
