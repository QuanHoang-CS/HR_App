using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net_core_web_api.Models.Domain;

namespace net_core_web_api.Data.Configurations
{
    // need the "using net_core_web_api.Models.Domain;" to be able to refer to the Country class
    public class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            /*
             * 1. Cannot chain HasKey() with Property().HasColumnName()
             * 2. Cannot indicate several properties inside a domain model in 1 call, have to do 1 by 1
             * 3. HasKey() is different from Required()
             * 4. HasKey() property is implicitly a Required() (a.k.a not null) property.
             */
            builder.ToTable("countries");
            builder.HasKey(x => x.CountryId);
            // builder.HasForeignKey(x => x.RegionId);

            builder.Property(x => x.CountryId)
                .HasColumnName("country_id")
                .HasMaxLength(2);

            builder.Property(x => x.CountryName)
                .HasColumnName("country_name")
                .HasMaxLength(40);

            builder.Property(x => x.RegionId)
                .HasColumnName("region_id")
                .IsRequired();
        }
    }
}
