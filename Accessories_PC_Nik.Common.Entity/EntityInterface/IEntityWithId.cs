﻿namespace Accessories_PC_Nik.Common.Entity.EntityInterface
{
    /// <summary>
    /// Сущность с идентификатором
    /// </summary>
    public interface IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
