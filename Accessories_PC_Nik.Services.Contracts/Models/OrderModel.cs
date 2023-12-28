
namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель заказов
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер пользователя товара
        /// </summary>
        public ClientModel Client { get; set; }

        /// <summary>
        /// Выбранная услуга
        /// </summary>
        public ServiceModel? Service { get; set; }

        /// <summary>
        /// Выбранный товар 
        /// </summary>
        public ComponentModel? Component { get; set; }

        /// <summary>
        /// Время заказа
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// Доставка
        /// </summary>
        public DeliveryModel? Delivery { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string? Comment { get; set; }
    }
}
