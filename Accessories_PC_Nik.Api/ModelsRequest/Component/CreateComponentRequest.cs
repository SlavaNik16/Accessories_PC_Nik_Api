using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Api.ModelsRequest.Component
{
    /// <summary>
    /// Модель запроса создания компонента 
    /// </summary>
    public class CreateComponentRequest
    {

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип компонента
        /// </summary>
        public TypeComponents TypeComponents { get; set; }

        /// <summary>
        /// Описания
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Тип материала
        /// </summary>
        public MaterialType MaterialType { get; set; }

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
