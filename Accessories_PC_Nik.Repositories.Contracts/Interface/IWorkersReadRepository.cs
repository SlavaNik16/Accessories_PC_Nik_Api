using Accessories_PC_Nik.Context.Contracts.Models;
namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Workers"/>
    /// </summary>
    public interface IWorkersReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Workers"/>
        /// </summary>
        Task<List<Workers>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Workers"/> по идентификатору
        /// </summary>
        Task<Workers?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
