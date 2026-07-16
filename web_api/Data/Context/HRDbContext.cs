using Microsoft.EntityFrameworkCore;
using MyApp.API.Models.Domain;

namespace MyApp.API.Data.Context
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
        
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<Employee> Employees{ get; set; }
        
        public DbSet<Job> Jobs { get; set; }
        
        public DbSet<Location> Locations { get; set; }
        //public DbSet<Regions> Regions { get; set; }
        
    }
}
