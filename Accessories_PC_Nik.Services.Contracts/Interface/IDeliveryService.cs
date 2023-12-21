using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IDeliveryService
    {
        /// <summary>
        /// Получить список всех <see cref="DeliveryModel"/>
        /// </summary>
        Task<IEnumerable<DeliveryModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="DeliveryModel"/> по идентификатору
        /// </summary>
        Task<DeliveryModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую доставку
        /// </summary>
        Task<DeliveryModel> AddAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую доставку
        /// </summary>
        Task<DeliveryModel> EditAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую доставку
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
