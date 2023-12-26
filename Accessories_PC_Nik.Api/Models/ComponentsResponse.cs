namespace Accessories_PC_Nik.Api.Models
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
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public string TypeComponents { get; set; }

        /// <summary>
        /// Описания
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Тип материала
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Цена за 1 шт.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Кол-во товара
        /// </summary>
        public int Count { get; set; }
    }
}
