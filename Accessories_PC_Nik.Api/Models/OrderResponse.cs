
namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель ответа сущности заказов
    /// </summary>
    public class OrderResponse
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
        public ComponentsResponse? ComponentsModel { get; set; }

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
        public DeliveryResponse DeliveryModel { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string Comment { get; set; }
    }
}
