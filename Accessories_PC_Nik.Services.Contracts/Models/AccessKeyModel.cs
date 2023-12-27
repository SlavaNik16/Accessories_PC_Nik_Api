using Accessories_PC_Nik.Services.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.Models
{
    /// <summary>
    /// Модель ключей доступа
    /// </summary>
    public class AccessKeyModel
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
        /// Тип получения уровня доступа при данном ключе <see cref="Key"/>
        /// </summary>
        public AccessLevelTypesModel Types { get; set; }

        /// <summary>
        /// Какой работник создает ключ доступа
        /// </summary>
        public WorkerModel Worker { get; set; }
    }
}
