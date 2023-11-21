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
