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

    }
}
