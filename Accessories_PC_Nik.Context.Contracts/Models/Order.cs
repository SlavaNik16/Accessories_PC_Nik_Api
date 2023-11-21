namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Order :BaseAuditEntity
    {
        /// <summary>
        /// Номер пользователя товара
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Cвязь один ко многим
        /// </summary>
        public Client? Client { get; set; }
        
        /// <summary>
        /// Выбранная услуга
        /// </summary>
        public Guid? ServiceId { get; set; }

        /// <summary>
        /// Cвязь один ко многим
        /// </summary>
        public Service? Service { get; set; }

        /// <summary>
        /// Выбранный товар 
        /// </summary>
        public Guid? ComponentId { get; set; }

        /// <summary>
        /// Cвязь один ко многим
        /// </summary>
        public Component? Component { get; set; }

        /// <summary>
        /// Время заказа
        /// </summary>
        public DateTime OrderTime { get; set; }
        
        /// <summary>
        /// Доставка
        /// </summary>
        public Guid? DeliveryId { get; set; }

        /// <summary>
        /// Cвязь один ко многим
        /// </summary>
        public Delivery? Delivery { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string? Comment { get; set; } = string.Empty;
    }
}
