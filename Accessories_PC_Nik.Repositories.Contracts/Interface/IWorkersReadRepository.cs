using Accessories_PC_Nik.Context.Contracts.Enums;
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
        /// Получить <see cref="Worker"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Worker>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, существует ли такой работник
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, используется ли этот номер
        /// </summary>
        Task<bool> AnyByNumberAsync(string number, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, может ли данный работник по id, создать ключ доступа по такому типу 
        /// </summary>
        Task<bool> AnyByWorkerWithTypeAsync(Guid id, AccessLevelTypes accessLevelTypes, CancellationToken cancellationToken);
    }
}
