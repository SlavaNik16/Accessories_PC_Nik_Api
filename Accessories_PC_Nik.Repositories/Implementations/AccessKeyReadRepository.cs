using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Common.Entity.Repositories;
using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class AccessKeyReadRepository : IAccessKeyReadRepository, IReadRepositoryAnchor
    {
        private readonly IDbRead reader;

        public AccessKeyReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<AccessKey>> IAccessKeyReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<AccessKey>()
                .NotDeletedAt()
                .OrderBy(x => x.Types)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<AccessKey?> IAccessKeyReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<AccessKey>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        async Task<AccessLevelTypes?> IAccessKeyReadRepository.GetAccessLevelByKeyAsync(Guid key, CancellationToken cancellationToken)
        {
            var accessKey = await reader.Read<AccessKey>()
                .NotDeletedAt()
                .FirstOrDefaultAsync(x => x.Key == key, cancellationToken);
            if (accessKey == null) return null;

            return accessKey.Types;
        }
    }
}
