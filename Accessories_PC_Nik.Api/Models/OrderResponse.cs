
namespace Accessories_PC_Nik.Api.Models
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
        /// FIO
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Название Услуги
        /// </summary>
        public string NameService { get; set; }

        /// <summary>
        /// Продолжительность услуги (возможно в часах)
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal PriceService { get; set; }

        /// <summary>
        /// Тип Компонента
        /// </summary>
        public string TypeComponents { get; set; }

        /// <summary>
        /// Стоимость компонента
        /// </summary>
        public decimal PriceComponent { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Время заказа
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// Откуда привезти
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Куда привезти
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Стоимость доставки
        /// </summary>
        public decimal PriceDelivery { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string? Comment { get; set; }
    }
}
