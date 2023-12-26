
namespace Accessories_PC_Nik.Api.ModelsRequest.Order
{
    /// <summary>
    /// Модель запроса изменения заказа 
    /// </summary>
    public class EditOrderRequest : CreateOrderRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
