using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.ModelRequest
{
    public class ComponentRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
