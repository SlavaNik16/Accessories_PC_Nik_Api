namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Базовый класс с аудитом (у каждого есть id и история с записями в бд)
    /// </summary>
    public abstract class BaseAuditEntity
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
        public DateTimeOffset UpdateAt { get; set; }

        /// <summary>
        /// Кем изменён
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTimeOffset? DeleteAt { get; set; }



    }
}
