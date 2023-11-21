using Accessories_PC_Nik.Services.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель компонентов пк
    /// </summary>
    public class ComponentsModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        public TypeComponentsModel TypeComponents { get; set; }

        /// <summary>
        /// Описания
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Тип материала
        /// </summary>
        public MaterialTypeModel MaterialType { get; set; }

        /// <summary>
        /// Цена за 1 шт.
        /// </summary>
        public decimal Price { get; set; }
    }
}
