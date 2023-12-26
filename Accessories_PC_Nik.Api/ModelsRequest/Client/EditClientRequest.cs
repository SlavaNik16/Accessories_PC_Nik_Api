namespace Accessories_PC_Nik.Api.ModelsRequest.Client
{
    /// <summary>
    /// Модель запроса изменения клиента 
    /// </summary>
    public class EditClientRequest : CreateClientRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
