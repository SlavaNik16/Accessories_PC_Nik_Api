using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration.TypeConfiguration
{
    internal class WorkerEntiry : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("TWorker");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.Number).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Series).IsRequired().HasMaxLength(12);
            builder.Property(x => x.IssuedAt).IsRequired();
            builder.Property(x => x.IssuedBy).IsRequired().HasMaxLength(300);
            builder.Property(x => x.DocumentType).IsRequired();
            builder.Property(x => x.AccessLevel).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();

            builder.HasIndex(x => x.Number)
                .IsUnique()
                .HasFilter($"{nameof(Worker.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Worker)}_" +
                             $"{nameof(Worker.Number)}");
        }
    }
}
