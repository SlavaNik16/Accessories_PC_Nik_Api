namespace Accessories_PC_Nik.Api.ModelsRequest.Service
{
    /// <summary>
    /// Модель запроса создания услуги 
    /// </summary>
    public class CreateServiceRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Продолжительность услуги (возможно в часах)
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Цена за услугу
        /// </summary>
        public decimal Price { get; set; }
    }
}
