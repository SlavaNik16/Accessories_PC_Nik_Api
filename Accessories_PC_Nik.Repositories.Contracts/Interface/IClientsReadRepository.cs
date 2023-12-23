using Accessories_PC_Nik.Context.Contracts.Models;
namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Client"/>
    /// </summary>
    public interface IClientsReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Client"/>
        /// </summary>
        Task<IReadOnlyCollection<Client>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Client"/> по идентификатору
        /// </summary>
        Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Client"/> по идентификатору
        /// </summary>
        Task<Dictionary<Guid, Client>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)

        /// <summary>
        /// Получить <see cref="Client"/> по идентификатору
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
