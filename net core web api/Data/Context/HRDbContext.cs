using Microsoft.EntityFrameworkCore;
using net_core_web_api.Models.Domain;

namespace net_core_web_api.Data.Context
{
    public class HRDbContext : DbContext
    {
        public HRDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDbContext).Assembly);
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        /*
        public DbSet<Dependants> Dependants { get; set; }
        public DbSet<Employees> Employees{ get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Regions> Regions { get; set; }
        */
    }
}
