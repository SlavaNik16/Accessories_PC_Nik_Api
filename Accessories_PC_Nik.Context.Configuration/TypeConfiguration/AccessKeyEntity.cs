using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration.EntityTypeConfiguration
{
    internal class AccessKeyEntity : IEntityTypeConfiguration<AccessKey>
    {
        public void Configure(EntityTypeBuilder<AccessKey> builder)
        {
            builder.ToTable("TAccessKey");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.Key).IsRequired();
            builder.Property(x => x.Types).IsRequired();

            builder.HasIndex(x => x.Types)
                .IsUnique()
                .HasFilter($"{nameof(AccessKey.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(AccessKey)}_" +
                                 $"{nameof(AccessKey.Id)}");
        }
    }
}
