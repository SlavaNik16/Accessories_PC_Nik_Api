using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration.TypeConfiguration
{
    internal class ServiceEntity : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("TService");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200); ;
            builder.Property(x => x.Duration).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder
               .HasMany(x => x.Order)
               .WithOne(x => x.Service)
               .HasForeignKey(x => x.ServiceId);

            builder.HasIndex(x => x.Name)
                .HasFilter($"{nameof(Service.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Service)}_" +
                             $"{nameof(Service.Name)}");

        }
    }
}
