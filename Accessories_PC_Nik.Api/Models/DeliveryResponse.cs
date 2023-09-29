namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель ответа сущности доставки
    /// </summary>
    public class DeliveryResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Согласен ли пользователь использовать доставку товара 
        /// </summary>
        public bool IsDelivery { get; set; } = false;

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
