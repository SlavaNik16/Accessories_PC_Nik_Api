namespace Accessories_PC_Nik.Api.ModelsRequest.Person
{
    public class EditDeliveryRequest : CreateDeliveryRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
