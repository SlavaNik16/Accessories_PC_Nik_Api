using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IServicesService
    {
        /// <summary>
        /// Получить список всех <see cref="ServicesModel"/>
        /// </summary>
        Task<IEnumerable<ServicesModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ServicesModel"/> по идентификатору
        /// </summary>
        Task<ServicesModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

      

    }
}
