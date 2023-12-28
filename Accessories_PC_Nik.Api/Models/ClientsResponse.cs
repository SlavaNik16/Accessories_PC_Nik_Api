namespace Accessories_PC_Nik.Api.Models
{
    /// <summary>
    /// Модель ответа сущности клиента
    /// </summary>
    public class ClientsResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// FIO
        /// </summary>
        public string FI0 { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; } = 0;
    }
}
