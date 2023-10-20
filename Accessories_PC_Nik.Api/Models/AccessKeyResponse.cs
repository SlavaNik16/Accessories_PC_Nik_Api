
using Accessories_PC_Nik.Api.Enums;

namespace Accessories_PC_Nik.Api.Models
{
    /// <summary>
    /// Модель ответа сущности ключей доступа
    /// </summary>
    public class AccessKeyResponse
    {
        /// <summary>
        /// Тип получения уровня доступа при данном ключе <see cref="Key"/>
        /// </summary>
        public string Types { get; set; }
    }
}
