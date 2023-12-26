using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration.TypeConfiguration
{
    internal class ComponentsEntity : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> builder)
        {
            builder.ToTable("TComponent");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
            builder.Property(x => x.TypeComponents).IsRequired();
            builder.Property(x => x.MaterialType).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Count).IsRequired();

            builder
                .HasMany(x => x.Order)
                .WithOne(x => x.Component)
                .HasForeignKey(x => x.ComponentId);

            builder.HasIndex(x => x.TypeComponents)
                .HasFilter($"{nameof(Component.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Component)}_" +
                             $"{nameof(Component.TypeComponents)}_" +
                             $"{nameof(Component.Id)}");

            builder.HasIndex(x => x.MaterialType)
               .HasFilter($"{nameof(Component.DeletedAt)} is null")
               .HasDatabaseName($"IX_{nameof(Component)}_" +
                                $"{nameof(Component.MaterialType)}_" +
                                $"{nameof(Component.Id)}");
        }
    }
}
