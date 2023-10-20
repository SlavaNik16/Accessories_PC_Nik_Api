using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Components"/>
    /// </summary>
    public interface IComponentsReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Components"/>
        /// </summary>
        Task<List<Components>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Components"/> по идентификатору
        /// </summary>
        Task<Components?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Components"/> по идентификатору
        /// </summary>
        Task<Dictionary<Guid, Components>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
