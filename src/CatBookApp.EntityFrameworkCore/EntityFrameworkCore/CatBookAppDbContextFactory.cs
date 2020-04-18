using CatBookApp.Configuration;
using CatBookApp.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CatBookApp.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class CatBookAppDbContextFactory : IDesignTimeDbContextFactory<CatBookAppDbContext>
    {
        public CatBookAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CatBookAppDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(CatBookAppConsts.ConnectionStringName)
            );

            return new CatBookAppDbContext(builder.Options);
        }
    }
}