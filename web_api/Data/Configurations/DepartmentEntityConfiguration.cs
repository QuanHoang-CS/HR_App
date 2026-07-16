using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyApp.API.Models.Domain;

namespace MyApp.API.Data.Configurations
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments");
            builder.HasKey(x => x.DepartmentId);

            builder.Property(x => x.DepartmentId)
                .HasColumnName("department_id");

            builder.Property(x => x.DepartmentName)
                .HasColumnName("department_name")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.LocationId)
                .HasColumnName("location_id");
               
        }
    }
}
