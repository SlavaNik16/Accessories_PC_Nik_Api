using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IWorkersService
    {
        /// <summary>
        /// Получить список всех <see cref="WorkersModel"/>
        /// </summary>
        Task<IEnumerable<WorkersModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="WorkersModel"/> по идентификатору
        /// </summary>
        Task<WorkersModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
