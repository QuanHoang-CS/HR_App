using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net_core_web_api.Models.Domain;

namespace net_core_web_api.Data.Configurations
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Try to set a reletionship of 1:1 or 1:many but later on
            //builder.HasOne()
            builder.ToTable("employees");
            builder.HasKey(x => x.EmployeeId);

            builder.Property(x => x.EmployeeId)
                .HasColumnName("employee_id")
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(20);

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(20);

            builder.Property(x => x.HireDate)
                .HasColumnName("hire_date")
                .IsRequired();

            builder.Property(x => x.JobId)
                .HasColumnName("job_id")
                .IsRequired();

            builder.Property(x => x.Salary)
                .HasPrecision(8, 2)         // Can also use .HasColumnType("decimal(8,2)")
                .IsRequired();

            builder.Property(x => x.ManagerId)
                .HasColumnName("manager_id");

            builder.Property(x => x.DepartmentId)
                .HasColumnName("department_id");
        }
    }
}
