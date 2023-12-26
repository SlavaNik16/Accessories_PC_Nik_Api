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
        Task<IReadOnlyCollection<Delivery>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Delivery"/> по идентификатору
        /// </summary>
        Task<Delivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Delivery"/> по идентификатору
        /// </summary>
        Task<Dictionary<Guid, Delivery>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, существует ли доставка по Id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
