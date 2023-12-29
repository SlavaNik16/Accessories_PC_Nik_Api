namespace Accessories_PC_Nik.Services.Contracts.ModelRequest
{
    public class DeliveryRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
