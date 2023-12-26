namespace Accessories_PC_Nik.Api.ModelsRequest.AccessKey
{
    /// <summary>
    /// Модель запроса изменения ключа уровня доступа
    /// </summary>
    public class EditAccessKeyRequest : CreateAccessKeyRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
