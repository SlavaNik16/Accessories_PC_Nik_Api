namespace Accessories_PC_Nik.Api.ModelsRequest.Order
{
    /// <summary>
    /// Модель запроса создания заказа 
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// Номер пользователя товара
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Выбранная услуга
        /// </summary>
        public Guid? ServiceId { get; set; }

        /// <summary>
        /// Выбранный товар 
        /// </summary>
        public Guid? ComponentId { get; set; }


        /// <summary>
        /// Время заказа
        /// </summary>
        public DateTime OrderTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Доставка
        /// </summary>
        public Guid? DeliveryId { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string? Comment { get; set; } = string.Empty;
    }
}
