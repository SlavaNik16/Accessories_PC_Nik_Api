using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accessories_PC_Nik.Context.Configuration.TypeConfiguration
{
    internal class ClientsEntity : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("TClient");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Patronymic).HasMaxLength(80);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Balance).IsRequired();

            builder
                .HasMany(x => x.Worker)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId);

            builder
                .HasMany(x => x.Order)
                .WithOne(x => x.Client)
                .HasForeignKey(x => x.ClientId);

            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasFilter($"{nameof(Client.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Client)}_" +
                                 $"{nameof(Client.Email)}");

            builder.HasIndex(x => x.Phone)
                .IsUnique()
                .HasFilter($"{nameof(Client.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Client)}_" +
                                 $"{nameof(Client.Phone)}");
        }
    }
}
