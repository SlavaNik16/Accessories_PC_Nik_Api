using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="AccessKey"/>
    /// </summary>
    public interface IAccessKeyReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="AccessKey"/>
        /// </summary>
        Task<IReadOnlyCollection<AccessKey>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="AccessKey"/> по идентификатору id
        /// </summary>
        Task<AccessKey?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="AccessLevelTypes"/> по идентификатору ключа
        /// </summary>
        Task<AccessLevelTypes?> GetAccessLevelByKeyAsync(Guid key, CancellationToken cancellationToken);
    }
}
