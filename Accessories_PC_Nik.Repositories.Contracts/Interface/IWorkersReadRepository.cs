using Accessories_PC_Nik.Context.Contracts.Models;
namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Worker"/>
    /// </summary>
    public interface IWorkersReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Worker"/>
        /// </summary>
        Task<IReadOnlyCollection<Worker>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Worker"/> по идентификатору
        /// </summary>
        Task<Worker?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, используется ли этот номер
        /// </summary>
        Task<bool> AnyByNumberAsync(string number, CancellationToken cancellationToken);
    }
}
