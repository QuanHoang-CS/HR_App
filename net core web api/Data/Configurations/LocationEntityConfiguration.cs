using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using net_core_web_api.Models.Domain;


namespace net_core_web_api.Data.Configurations
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");
            builder.HasKey("LocationId");

            builder.Property("LocationId")
                .HasColumnName("location_id")
                .IsRequired();

            builder.Property("StreetAddress")
                .HasColumnName("street_address")
                .HasMaxLength(40);

            builder.Property("PostalCode")
                .HasColumnName("postal_code")
                .HasMaxLength(12);

            builder.Property("City")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property("StateProvince")
                .HasColumnName("state_province")
                .HasMaxLength(25);

            builder.Property("CountryId")
                .HasColumnName("country_id")
                .HasMaxLength(2)
                .IsRequired();


        }
    }
}
