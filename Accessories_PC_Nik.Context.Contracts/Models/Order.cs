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
        public Guid Client_id { get; set; }
        
        /// <summary>
        /// Выбранная услуга
        /// </summary>
        public Guid? Services_id { get; set; }

        /// <summary>
        /// Выбранный товар 
        /// </summary>
        public Guid? Components_id { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Count { get; set; } = 0;
        /// <summary>
        /// Время заказа
        /// </summary>
        public DateTime OrderTime { get; set; }
        
        /// <summary>
        /// Доставка
        /// </summary>
        public Guid? Delivery_id { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string Comment { get; set; } = string.Empty;
    }
}
