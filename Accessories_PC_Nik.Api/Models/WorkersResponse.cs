using Accessories_PC_Nik.Api.Enums;
namespace Accessories_PC_Nik.Api.Models
{
    /// <summary>
    /// Модель ответа сущности сотрудникам
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
        public string DocumentType { get; set; }

        /// <summary>
        /// Тип уровня доступа
        /// </summary>
        public string AccessLevel { get; set; }

    }
}
