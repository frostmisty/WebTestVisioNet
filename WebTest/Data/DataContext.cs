using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebTest.Entity;

namespace WebTest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<MsJenis> MsJenis { get; set; }
        public DbSet<TrKasir> TrKasir { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
