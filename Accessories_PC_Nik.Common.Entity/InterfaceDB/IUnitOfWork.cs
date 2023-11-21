namespace Accessories_PC_Nik.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Определяет интерфейс для Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Асинхронно сохраняет все изменения в бд
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
