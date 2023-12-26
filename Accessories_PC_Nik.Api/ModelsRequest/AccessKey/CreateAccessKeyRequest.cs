using Accessories_PC_Nik.Services.Contracts.Enums;

namespace Accessories_PC_Nik.Api.ModelsRequest.AccessKey
{
    /// <summary>
    /// Модель запроса создания ключа уровня доступа
    /// </summary>
    public class CreateAccessKeyRequest
    {
        /// <summary>
        /// Тип получения уровня доступа при данном ключе <see cref="Key"/>
        /// </summary>
        public AccessLevelTypesModel Types { get; set; }
    }
}
