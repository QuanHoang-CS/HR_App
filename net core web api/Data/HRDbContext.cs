using Microsoft.EntityFrameworkCore;
using net_core_web_api.Models.Domain;

namespace net_core_web_api.Data
{
    public class HRDbContext : DbContext
    {
        public HRDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Dependants> Dependants { get; set; }
        public DbSet<Employees> Employees{ get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Regions> Regions { get; set; }
    }
}
