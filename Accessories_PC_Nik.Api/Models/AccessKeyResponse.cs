namespace Accessories_PC_Nik.Api.Models
{
    /// <summary>
    /// Модель ответа сущности ключей доступа
    /// </summary>
    public class AccessKeyResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ключ уровня доступа
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Уровень привилегий
        /// </summary>
        public string Types { get; set; }
    }
}
