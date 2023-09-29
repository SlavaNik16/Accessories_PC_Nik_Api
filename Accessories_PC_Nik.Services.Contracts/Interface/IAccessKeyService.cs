using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IAccessKeyService
    {
        /// <summary>
        /// Получить список всех <see cref="AccessKeyModel"/>
        /// </summary>
        Task<IEnumerable<AccessKeyModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="AccessKeyModel"/> по идентификатору
        /// </summary>
        Task<AccessKeyModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
