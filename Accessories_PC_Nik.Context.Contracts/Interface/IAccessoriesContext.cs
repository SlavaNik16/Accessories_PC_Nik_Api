using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Context.Contracts.Interface
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface IAccessoriesContext
    {
        IEnumerable<Clients> Clients { get; }
        IEnumerable<Workers> Workers { get; }
        IEnumerable<Services> Services { get; }
        IEnumerable<Order> Order { get; }
        IEnumerable<Delivery> Delivery { get; }
        IEnumerable<AccessKey> AccessKey { get; }
        IEnumerable<Components> Components { get; }
    }
}
