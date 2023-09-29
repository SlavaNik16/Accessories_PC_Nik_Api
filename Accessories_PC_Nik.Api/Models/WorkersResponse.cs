using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель ответа сущности сотрудникам
    /// (
    /// неужто надо дублировать и типы enum,
    /// чтобы не сделать модель зависимой от них
    /// )
    /// </summary>
    public class WorkersResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Серия документа
        /// </summary>
        public string Series { get; set; } = string.Empty;

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string IssuedBy { get; set; } = string.Empty;

        /// <summary>
        /// Тип документа на работу
        /// </summary>
        public DocumentTypes DocumentType { get; set; }

        /// <summary>
        /// Тип уровня доступа
        /// </summary>
        public AccessLevelTypes AccessLevel { get; set; } = AccessLevelTypes.None;

    }
}
