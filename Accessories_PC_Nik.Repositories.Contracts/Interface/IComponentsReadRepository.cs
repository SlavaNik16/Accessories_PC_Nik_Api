using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Component"/>
    /// </summary>
    public interface IComponentsReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Component"/>
        /// </summary>
        Task<List<Component>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Component"/> по идентификатору
        /// </summary>
        Task<Component?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Component"/> по идентификатору
        /// </summary>
        Task<Dictionary<Guid, Component>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
