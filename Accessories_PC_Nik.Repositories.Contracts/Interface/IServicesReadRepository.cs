using Accessories_PC_Nik.Context.Contracts.Models;
namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Service"/>
    /// </summary>
    public interface IServicesReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Service"/>
        /// </summary>
        Task<List<Service>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Service"/> по идентификатору
        /// </summary>
        Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ServicesModel"/> по идентификатору
        /// </summary>
        Task<Dictionary<Guid,Service>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
