namespace Accessories_PC_Nik.Api.ModelsRequest.Worker
{
    /// <summary>
    /// Модель запроса изменения работника 
    /// </summary>
    public class EditWorkerRequest : CreateWorkerRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
