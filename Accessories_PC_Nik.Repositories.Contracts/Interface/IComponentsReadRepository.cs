using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Components"/>
    /// </summary>
    public interface IComponentsReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Components"/>
        /// </summary>
        Task<List<Components>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Components"/> по идентификатору
        /// </summary>
        Task<Components?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
