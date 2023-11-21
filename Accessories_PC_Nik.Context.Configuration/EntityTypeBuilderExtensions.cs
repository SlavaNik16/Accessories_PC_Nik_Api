using Accessories_PC_Nik.Common.Entity.EntityInterface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration
{
    /// <summary>
    /// Методы расширения для <see cref="EntityTypeBuilder"/>
    /// </summary>
     internal static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Задаёт конфигурацию свойств аудита для сущности <inheritdoc cref="BaseAuditEntity"/>
        /// </summary>
        public static void PropertyAuditConfiguration<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : BaseAuditEntity
        {
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.UpdatedBy).IsRequired().HasMaxLength(200);
        }

        /// <summary>
        /// Задаёт конфигурацию ключа для идентификатора <see cref="IEntityWithId.Id"/>
        /// </summary>
        public static void HasIdAsKey<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IEntityWithId
            => builder.HasKey(x => x.Id);
    }
}
