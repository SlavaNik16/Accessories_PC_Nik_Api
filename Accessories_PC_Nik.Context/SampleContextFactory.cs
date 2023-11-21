using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Accessories_PC_Nik.Context
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<AccessoriesContext>
    {
        public AccessoriesContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<AccessoriesContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new AccessoriesContext(options);
        }
    }
    
}
