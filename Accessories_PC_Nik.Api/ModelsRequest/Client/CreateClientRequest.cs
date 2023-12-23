using Accessories_PC_Nik.Api.Models.Enums;
using Accessories_PC_Nik.Services.Contracts.Models;

namespace Accessories_PC_Nik.Api.ModelsRequest.Document
{
    public class CreateClientRequest
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }

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
        public decimal Balance { get; set; }

    }
}
