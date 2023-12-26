namespace Accessories_PC_Nik.Context.Contracts.Models
{

    /// <summary>
    /// Услуги
    /// </summary>
    public class Service : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Продолжительность услуги (возможно в часах)
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Цена за услугу
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// навигация для связи 1 ко многим
        /// </summary>
        public ICollection<Order> Order { get; set; }

    }
}
