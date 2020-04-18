using Microsoft.EntityFrameworkCore;

namespace CatBookApp.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<CatBookAppDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for CatBookAppDbContext */
            dbContextOptions.UseMySql(connectionString);
        }
    }
}
