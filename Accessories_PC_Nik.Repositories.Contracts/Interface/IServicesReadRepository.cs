using Accessories_PC_Nik.Context.Contracts.Models;
namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Services"/>
    /// </summary>
    public interface IServicesReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Services"/>
        /// </summary>
        Task<List<Services>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="Services"/> по идентификатору
        /// </summary>
        Task<Services?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
