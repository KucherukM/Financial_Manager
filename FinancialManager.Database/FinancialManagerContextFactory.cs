using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FinancialManagerApp.Models
{
    public class FinancialManagerContextFactory : IDesignTimeDbContextFactory<FinancialManagerContext>
    {
        public FinancialManagerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinancialManagerContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("FinancialManagerDatabase");
            optionsBuilder.UseSqlServer(connectionString);

            return new FinancialManagerContext(optionsBuilder.Options);
        }
    }
}
