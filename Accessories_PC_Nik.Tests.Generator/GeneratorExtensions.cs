using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Tests.Generator
{
    public static class GeneratorExtensions
    {
        public static void BaseAuditEntity<TEntity>(this TEntity entity) where TEntity : BaseAuditEntity
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedBy = $"CreatedBy{Guid.NewGuid():N}";
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}";
        }
    }
}
