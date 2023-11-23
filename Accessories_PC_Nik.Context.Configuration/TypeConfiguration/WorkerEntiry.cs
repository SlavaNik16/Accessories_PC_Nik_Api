using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories_PC_Nik.Context.Configuration.TypeConfiguration
{
    internal class WorkerEntiry : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("TWorker");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Series).IsRequired();
            builder.Property(x => x.IssuedAt).IsRequired();
            builder.Property(x => x.IssuedBy).IsRequired();
            builder.Property(x => x.DocumentType).IsRequired();
            builder.Property(x => x.AccessLevel).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();

            builder.HasIndex(x => x.Number)
                .IsUnique()
                .HasFilter($"{nameof(Worker.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Worker)}_" +
                             $"{nameof(Worker.Number)}");

            builder.HasIndex(x => x.Series)
                 .IsUnique()
                 .HasFilter($"{nameof(Worker.DeletedAt)} is null")
                 .HasDatabaseName($"IX_{nameof(Worker)}_" +
                              $"{nameof(Worker.Series)}");

        }
    }
}
