using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Комплектующие ПК
    /// </summary>
    public class Component : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public TypeComponents TypeComponents { get; set; } = TypeComponents.Processor;

        /// <summary>
        /// Описания
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Тип материала
        /// </summary>
        public MaterialType MaterialType { get; set; } = MaterialType.None;

        /// <summary>
        /// Цена за 1 шт.
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Count { get; set; } = 1;

        /// <summary>
        /// навигация для связи 1 ко многим
        /// </summary>
        public ICollection<Order> Order { get; set; }

    }
}
