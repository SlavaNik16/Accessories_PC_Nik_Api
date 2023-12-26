namespace Accessories_PC_Nik.Api.ModelsRequest.Delivery
{
    /// <summary>
    /// Модель запроса изменения доставки 
    /// </summary>
    public class EditDeliveryRequest : CreateDeliveryRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
