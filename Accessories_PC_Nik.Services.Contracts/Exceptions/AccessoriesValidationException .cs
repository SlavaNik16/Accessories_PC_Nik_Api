using Accessories_PC_Nik.Shared;

namespace Accessories_PC_Nik.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибки валидации
    /// </summary>
    public class AccessoriesValidationException : AccessoriesException
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AccessoriesValidationException"/>
        /// </summary>
        public AccessoriesValidationException(IEnumerable<InvalidateItemModel> errors)
        {
            Errors = errors;
        }
    }
}
