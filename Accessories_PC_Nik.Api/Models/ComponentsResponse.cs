using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель ответа сущности компонентов пк
    /// </summary>
    public class ComponentsResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        public TypeComponents typeComponents { get; set; }

        /// <summary>
        /// Описания
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Тип материала
        /// </summary>
        public MaterialType MaterialType { get; set; }

        /// <summary>
        /// Цена за 1 шт.
        /// </summary>
        public decimal Price { get; set; }
    }
}
