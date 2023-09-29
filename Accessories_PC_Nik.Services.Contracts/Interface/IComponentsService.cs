using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IComponentsService
    {
        /// <summary>
        /// Получить список всех <see cref="ComponentsModel"/>
        /// </summary>
        Task<IEnumerable<ComponentsModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ComponentsModel"/> по идентификатору
        /// </summary>
        Task<ComponentsModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
