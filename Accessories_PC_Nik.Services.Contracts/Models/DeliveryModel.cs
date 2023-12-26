namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель доставки
    /// </summary>
    public class DeliveryModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Откуда привезти
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Куда привезти
        /// </summary>
        public DateTimeOffset To { get; set; }

        /// <summary>
        /// Стоимость доставки
        /// </summary>
        public decimal Price { get; set; }
    }
}
