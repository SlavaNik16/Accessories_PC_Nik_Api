namespace Accessories_PC_Nik.Services.Contracts.Models
{   /// <summary>
    /// Модель услуги
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
        public string Name { get; set; } 

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; } 

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
