namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Покупатели, клиенты
    /// </summary>
    public class Clients : BaseAuditEntity
    {

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; } = 0;
    }
}
