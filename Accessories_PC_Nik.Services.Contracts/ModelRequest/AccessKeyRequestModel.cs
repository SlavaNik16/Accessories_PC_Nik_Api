using Accessories_PC_Nik.Context.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.ModelRequest
{
    public class AccessKeyRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тип получения уровня доступа при данном ключе
        /// </summary>
        public AccessLevelTypes Types { get; set; }

        /// <summary>
        /// Какой работник создает ключ доступа
        /// </summary>
        public Guid WorkerId { get; set; }
    }
}
