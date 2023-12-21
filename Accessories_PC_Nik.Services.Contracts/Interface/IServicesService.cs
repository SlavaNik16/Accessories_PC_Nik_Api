using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IServicesService
    {
        /// <summary>
        /// Получить список всех <see cref="ServiceModel"/>
        /// </summary>
        Task<IEnumerable<ServiceModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ServiceModel"/> по идентификатору
        /// </summary>
        Task<ServiceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую услугу
        /// </summary>
        Task<ServiceModel> AddAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую услуги
        /// </summary>
        Task<ServiceModel> EditAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую услугу
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    }
}
