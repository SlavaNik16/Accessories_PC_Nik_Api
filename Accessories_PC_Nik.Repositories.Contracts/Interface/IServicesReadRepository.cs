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
        Task<IReadOnlyCollection<Service>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Service"/> по идентификатору
        /// </summary>
        Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ServicesModel"/> по идентификатору
        /// </summary>
        Task<Dictionary<Guid, Service>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, существует ли такое имя
        /// </summary>
        Task<bool> AnyByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, изменяем ли мы имя, совпадающее по такому id
        /// </summary>
        Task<bool> AnyByNameIsIdAsync(string name, Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить ответ, существует ли компонент по Id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
