namespace Accessories_PC_Nik.Api.ModelsRequest.Delivery
{

    /// <summary>
    /// Модель запроса создания доставки 
    /// </summary>
    public class CreateDeliveryRequest
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
        public decimal Price { get; set; }
    }
}
