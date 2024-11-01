using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace FinancialManagerApp.Models
{

    namespace FinancialManagerApp.Models
    {
        public class FinancialManagerContextFactory : IDesignTimeDbContextFactory<FinancialManagerContext>
        {
            public FinancialManagerContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<FinancialManagerContext>();

                
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FInancialManagerDB;Integrated Security=True;Connect Timeout=2;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

                return new FinancialManagerContext(optionsBuilder.Options);
            }
        }
    }

}

