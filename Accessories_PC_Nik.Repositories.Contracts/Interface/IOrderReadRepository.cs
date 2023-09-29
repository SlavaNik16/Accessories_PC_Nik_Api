using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Order"/>
    /// </summary>
    public interface IOrderReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Order"/>
        /// </summary>
        Task<List<Order>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Order"/> по идентификатору
        /// </summary>
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
