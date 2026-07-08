using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net_core_web_api.Models.Domain;

namespace net_core_web_api.Data.Configurations
{
    public class JobEntityConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("jobs");
            builder.HasKey("JobId");

            builder.Property(x => x.JobId)
                .HasColumnName("job_id")
                .IsRequired();

            builder.Property(x => x.JobTitle)
                .HasColumnName("job_title")
                .HasMaxLength(35)
                .IsRequired();

            builder.Property(x => x.MinSalary)
                .HasColumnName("min_salary")
                .HasPrecision(8,2);

            builder.Property(x => x.MaxSalary)
                .HasColumnName("max_salary")
                .HasPrecision(8,2);    
        }
    }
}
