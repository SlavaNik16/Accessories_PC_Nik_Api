
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
        public Guid Client_id { get; set; }

        /// <summary>
        /// Выбранная услуга
        /// </summary>
        public ServicesModel? ServicesModel { get; set; }

        /// <summary>
        /// Выбранный товар 
        /// </summary>
        public ComponentsModel? ComponentsModel { get; set; }

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
        public DeliveryModel DeliveryModel { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string Comment { get; set; }
    }
}
