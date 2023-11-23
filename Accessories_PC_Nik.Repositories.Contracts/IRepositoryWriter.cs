using System.Diagnostics.CodeAnalysis;

namespace Accessories_PC_Nik.Repositories.Contracts
{
    public interface IRepositoryWriter<in TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add([NotNull] TEntity entity);

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update([NotNull] TEntity entity);

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete([NotNull] TEntity entity);
    }
}
