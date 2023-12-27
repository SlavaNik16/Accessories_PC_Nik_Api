namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Доставка компонентов
    /// </summary>
    public class Delivery : BaseAuditEntity
    {

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
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// навигация для связи 1 ко многим
        /// </summary>
        public ICollection<Order> Order { get; set; }

    }
}
