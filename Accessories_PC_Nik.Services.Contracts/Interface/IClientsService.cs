using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IClientsService
    {
        /// <summary>
        /// Получить список всех <see cref="ClientsModel"/>
        /// </summary>
        Task<IEnumerable<ClientsModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ClientsModel"/> по идентификатору
        /// </summary>
        Task<ClientsModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
