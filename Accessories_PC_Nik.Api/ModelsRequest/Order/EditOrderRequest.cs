using Accessories_PC_Nik.Api.ModelsRequest.TimeTableItemRequest;

namespace Accessories_PC_Nik.Api.ModelsRequest.TimeTableItem
{
    public class EditOrderRequest : CreateOrderRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
