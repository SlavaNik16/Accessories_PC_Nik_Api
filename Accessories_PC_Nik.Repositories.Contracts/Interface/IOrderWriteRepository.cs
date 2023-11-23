using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий записи <see cref="Order"/>
    /// </summary>
    public interface IOrderWriteRepository : IRepositoryWriter<Order>
    {
    }
}
