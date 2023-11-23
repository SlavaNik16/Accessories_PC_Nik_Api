using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration.TypeConfiguration
{
    internal class OrderEntity : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("TOrder");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.OrderTime).IsRequired();

            builder.HasIndex(x => x.OrderTime)
                .HasFilter($"{nameof(Order.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Order)}_" +
                             $"{ nameof(Order.OrderTime)}_" +
                             $"{nameof(Order.Id)}");

        }
    }
}
