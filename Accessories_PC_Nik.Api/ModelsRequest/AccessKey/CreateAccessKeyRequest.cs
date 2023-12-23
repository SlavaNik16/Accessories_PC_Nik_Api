using Accessories_PC_Nik.Services.Contracts.Enums;

namespace Accessories_PC_Nik.Api.ModelsRequest.Discipline
{
    /// <summary>
    /// Модель запроса создания дисциплины
    /// </summary>
    public class CreateAccessKeyRequest
    {
        /// <summary>
        /// Ключ уровня доступа
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Тип получения уровня доступа при данном ключе <see cref="Key"/>
        /// </summary>
        public AccessLevelTypesModel Types { get; set; }
    }
}
