using Accessories_PC_Nik.Repositories.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class AccessKeyService : IAccessKeyService
    {
        private readonly IAccessKeyReadRepository accessKeyReadRepository;
        public AccessKeyService(IAccessKeyReadRepository accessKeyReadRepository)
        {
            this.accessKeyReadRepository = accessKeyReadRepository;
        }
        async Task<IEnumerable<AccessKeyModel>> IAccessKeyService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await accessKeyReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => new AccessKeyModel
            {
                Id = x.Id,
                Key = x.Key,
                Types = x.Types,
            });
        }

        async Task<AccessKeyModel?> IAccessKeyService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await accessKeyReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return new AccessKeyModel
            {
                Id = item.Id,
                Key = item.Key,
                Types = item.Types,
            };
        }
    }
}
