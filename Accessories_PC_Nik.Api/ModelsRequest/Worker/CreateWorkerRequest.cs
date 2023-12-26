using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Api.ModelsRequest.Worker
{
    /// <summary>
    /// Модель запроса создания работника 
    /// </summary>
    public class CreateWorkerRequest
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Серия документа
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string IssuedBy { get; set; }

        /// <summary>
        /// Тип документа на работу
        /// </summary>
        public DocumentTypes DocumentType { get; set; }

        /// <summary>
        /// Тип уровня доступа
        /// </summary>
        public AccessLevelTypes AccessLevel { get; set; }

        /// <summary>
        /// Данные клиента
        /// </summary>
        public Guid ClientId { get; set; }
    }
}
