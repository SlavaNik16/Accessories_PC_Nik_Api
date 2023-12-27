using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Configuration.Anchor;
using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project Accessories_PC_Nik.Context\Accessories_PC_Nik.Context.csproj
    /// 4) dotnet ef database update --project Accessories_PC_Nik.Context\Accessories_PC_Nik.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --Accessories_PC_Nik.Context\Accessories_PC_Nik.Context.csproj
    /// </remarks>
    /// 
    public class AccessoriesContext : DbContext,
        IAccessoriesContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<AccessKey> AccessKeys { get; set; }

        public DbSet<Component> Components { get; set; }

        public AccessoriesContext(DbContextOptions<AccessoriesContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }
    }
}
