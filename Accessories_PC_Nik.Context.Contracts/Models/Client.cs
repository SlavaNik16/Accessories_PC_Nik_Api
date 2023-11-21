namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Покупатели, клиенты
    /// </summary>
    public class Client : BaseAuditEntity
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

        /// <summary>
        /// навигация для связи 1 ко многим
        /// </summary>
        public ICollection<Order> Order { get; set; }

        /// <summary>
        /// навигация для связи 1 ко многим
        /// </summary>
        public ICollection<Worker> Worker { get; set; }
    }
}
