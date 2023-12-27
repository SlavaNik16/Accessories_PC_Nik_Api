using Accessories_PC_Nik.Services.Contracts.Enums;

namespace Accessories_PC_Nik.Services.Contracts.ModelRequest
{
    public class AccessKeyRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тип получения уровня доступа при данном ключе <see cref="Key"/>
        /// </summary>
        public AccessLevelTypesModel Types { get; set; }
    }
}
