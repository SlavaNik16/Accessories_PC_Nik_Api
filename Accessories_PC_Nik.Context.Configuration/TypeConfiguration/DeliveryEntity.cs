using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration.TypeConfiguration
{
    internal class DeliveryEntity : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("TDelivery");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.From).IsRequired();
            builder.Property(x => x.To).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            builder
               .HasMany(x => x.Order)
               .WithOne(x => x.Delivery)
               .HasForeignKey(x => x.DeliveryId);

            builder.HasIndex(x => x.From)
                .HasFilter($"{nameof(Delivery.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Delivery)}_" +
                             $"{nameof(Delivery.From)}");

        }
    }
}
