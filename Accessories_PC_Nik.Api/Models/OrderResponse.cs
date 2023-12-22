
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
        /// Тип Компонента
        /// </summary>
        public string TypeComponents { get; set; }

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
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Куда привезти
        /// </summary>
        public DateTimeOffset To { get; set; }

        /// <summary>
        /// Стоимость доставки
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Комментарий к заказу
        /// </summary>
        public string? Comment { get; set; }
    }
}
