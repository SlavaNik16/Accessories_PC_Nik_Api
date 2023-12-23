namespace Accessories_PC_Nik.Api.ModelsRequest.Employee
{
    public class EditWorkerRequest : CreateWorkerRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
