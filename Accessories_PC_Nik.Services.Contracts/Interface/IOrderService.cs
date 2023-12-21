using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IOrderService
    {
        /// <summary>
        /// Получить список всех <see cref="OrderModel"/>
        /// </summary>
        Task<IEnumerable<OrderModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="OrderModel"/> по идентификатору
        /// </summary>
        Task<OrderModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый заказ
        /// </summary>
        Task<OrderModel> AddAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий заказ
        /// </summary>
        Task<OrderModel> EditAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий заказ
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
