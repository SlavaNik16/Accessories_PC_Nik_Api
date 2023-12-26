namespace Accessories_PC_Nik.Api.ModelsRequest.Component
{

    /// <summary>
    /// Модель запроса изменения компонента 
    /// </summary>
    public class EditComponentRequest : CreateComponentRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
