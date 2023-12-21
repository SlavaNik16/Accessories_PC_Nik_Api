using Accessories_PC_Nik.Services.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель сотрудникам
    /// </summary>
    public class WorkersModel
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
        public string? IssuedBy { get; set; }

        /// <summary>
        /// Тип документа на работу
        /// </summary>
        public DocumentTypesModel DocumentType { get; set; }

        /// <summary>
        /// Тип уровня доступа
        /// </summary>
        public AccessLevelTypesModel AccessLevel { get; set; }

        /// <summary>
        /// Данные клиента
        /// </summary>
        public ClientsModel Clients { get; set; }

    }
}
