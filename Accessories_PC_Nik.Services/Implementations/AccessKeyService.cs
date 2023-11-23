using Accessories_PC_Nik.Repositories.Contracts.Interface.AccessKeyRead;
using Accessories_PC_Nik.Services.Anchors;
using Accessories_PC_Nik.Services.Contracts.Interface;
using Accessories_PC_Nik.Services.Contracts.Models;
using AutoMapper;

namespace Accessories_PC_Nik.Services.Implementations
{
    public class AccessKeyService : IAccessKeyService, IServiceAnchor
    {
        private readonly IAccessKeyReadRepository accessKeyReadRepository;
        private readonly IMapper mapper;
        public AccessKeyService(IAccessKeyReadRepository accessKeyReadRepository, 
            IMapper mapper)
        {
            this.accessKeyReadRepository = accessKeyReadRepository;
            this.mapper = mapper;
        }
        async Task<IEnumerable<AccessKeyModel>> IAccessKeyService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await accessKeyReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<AccessKeyModel>>(result);
        }

        async Task<AccessKeyModel?> IAccessKeyService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await accessKeyReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null) return null;

            return mapper.Map<AccessKeyModel>(item);
        }
    }
}
