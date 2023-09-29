using Accessories_PC_Nik.Context.Contracts.Models;
namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Clients"/>
    /// </summary>
    public interface IClientsReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Clients"/>
        /// </summary>
        Task<List<Clients>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Clients"/> по идентификатору
        /// </summary>
        Task<Clients?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
