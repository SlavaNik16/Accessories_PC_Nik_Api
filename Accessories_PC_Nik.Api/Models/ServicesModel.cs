namespace Accessories_PC_Nik.Services.Contracts.Models
{   /// <summary>
    /// Модель ответа сущности услуги
    /// </summary>
    public class ServicesModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Продолжительность услуги (возможно в часах)
        /// </summary>
        public DateTimeOffset Duration { get; set; }

        /// <summary>
        /// Цена за услугу
        /// </summary>
        public decimal Price { get; set; }
    }
}
