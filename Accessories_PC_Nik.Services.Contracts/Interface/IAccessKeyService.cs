using Accessories_PC_Nik.Context.Contracts.Enums;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Services.Contracts.Interface
{
    public interface IAccessKeyService
    {
        /// <summary>
        /// Получить список всех <see cref="AccessKeyModel"/>
        /// </summary>
        Task<IEnumerable<AccessKeyModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="AccessKeyModel"/> по идентификатору
        /// </summary>
        Task<AccessKeyModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый ключ
        /// </summary>
        Task<AccessKeyModel> AddAsync(AccessLevelTypes accessKeyTypesModel, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий ключ
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
