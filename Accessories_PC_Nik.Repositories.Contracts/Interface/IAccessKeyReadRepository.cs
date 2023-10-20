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
        Task<List<AccessKey>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="AccessKey"/> по идентификатору
        /// </summary>
        Task<AccessKey?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
