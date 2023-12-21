using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories_PC_Nik.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public class AccessoriesException : Exception
    {

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AccessoriesException"/> без параметров
        /// </summary>
        public AccessoriesException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AccessoriesException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected AccessoriesException(string message)
            : base(message) { }
    }
}
