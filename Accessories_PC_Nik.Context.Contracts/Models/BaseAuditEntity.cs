using Accessories_PC_Nik.Common.Entity;
using Accessories_PC_Nik.Common.Entity.EntityInterface;

namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Базовый класс с аудитом (у каждого есть id и история с записями в бд)
    /// </summary>
    public abstract class BaseAuditEntity : IEntity,
        IEntityWithId,
        IEntityAuditCreated,
        IEntityAuditUpdated,
        IEntityAuditDeleted
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Когда создан
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Кем создан
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Когда изменен
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Кем изменён
        /// </summary>
        public string UpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }



    }
}
