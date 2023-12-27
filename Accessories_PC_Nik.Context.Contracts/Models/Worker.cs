using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    public class Worker : BaseAuditEntity
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
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string IssuedBy { get; set; }

        /// <summary>
        /// Тип документа на работу
        /// </summary>
        public DocumentTypes DocumentType { get; set; } = DocumentTypes.None;

        /// <summary>
        /// Тип уровня доступа
        /// </summary>
        public AccessLevelTypes AccessLevel { get; set; } = AccessLevelTypes.None;

        /// <summary>
        /// Данные клиента
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Cвязь один ко многим
        /// </summary>
        public Client? Client { get; set; }

        /// <summary>
        /// навигация для связи 1 ко многим
        /// </summary>
        public ICollection<AccessKey> AccessKeys { get; set; }




    }
}
