using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Context.Contracts.Models
{
    /// <summary>
    /// Ключ доступа для получения привилегий
    /// </summary>
    public class AccessKey : BaseAuditEntity
    {
        /// <summary>
        /// Ключ уровня доступа
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Тип получения уровня доступа при данном ключе <see cref="Key"/>
        /// </summary>
        public AccessLevelTypes Types { get; set; }

        /// <summary>
        /// Какой работник создает ключ доступа
        /// </summary>
        public Guid WorkerId { get; set; }

        public Worker Worker { get; set; }
    }
}
