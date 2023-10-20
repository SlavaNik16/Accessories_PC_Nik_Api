using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Комплектующие ПК
    /// </summary>
    public class Components :BaseAuditEntity
    {

        /// <summary>
        /// Тип
        /// </summary>
        public TypeComponents TypeComponents { get; set; }

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
