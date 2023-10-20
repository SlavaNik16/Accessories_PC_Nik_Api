using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Delivery"/>
    /// </summary>
    public interface IDeliveryReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Delivery"/>
        /// </summary>
        Task<List<Delivery>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Delivery"/> по идентификатору
        /// </summary>
        Task<Delivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Delivery"/> по идентификатору
        /// </summary>
        Task<Dictionary<Guid, Delivery>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
