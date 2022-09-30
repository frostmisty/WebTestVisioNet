using Microsoft.EntityFrameworkCore;

namespace WebTest.Data
{
    public static class Dependencies
    {
        public static void ConfigurationService(IConfiguration configuration,IServiceCollection service)
        {
            service.AddDbContext<DataContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
