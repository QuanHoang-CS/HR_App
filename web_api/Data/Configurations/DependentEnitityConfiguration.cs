using MyApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MyApp.API.Data.Configurations
{
    public class DependentEnitityConfiguration : IEntityTypeConfiguration<Dependent>
    {
        public void Configure(EntityTypeBuilder<Dependent> builder)
        {
            builder.ToTable("dependents");

            builder.HasKey("Id");

            builder.Property(x => x.Id)
                .HasColumnName("dependent_id");

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Relationship)
                .HasColumnName("relationship")
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(x => x.EmployeeId)
                .HasColumnName("employee_id")
                .IsRequired();
        }
    }
}
