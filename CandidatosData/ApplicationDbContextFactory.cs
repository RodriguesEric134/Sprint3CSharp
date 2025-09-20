using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CandidatosData
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseOracle("User Id=rm550249;Password=231204;Data Source=oracle.fiap.com.br:1521/orcl;");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
