
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_core_web_api.Models.Domain
{

    /*
     * Given how wildly different the name of columns from our DB is compared to the properties' name here
     * EF core will not be able to map the properties to columns.
     * 2 ways to fix this: use Data Anotations directly in the data domain files (with attribute like [Key] or [Column("country_id")])
     * like what we'r doing below, or set up a data configurations folder with EntityConfiguration Classes
     * 
     * The second approach also has 2 ways. First is to overload the OnModelCreating methods directly with all the annotations, the second way is to have a dedicated
     * configuration folder to indicate the annotations
     * 
     * In the 2nd way of the 2nd approach, we split Data folder into Configuration folder and Context folder, Context contains our DbContext.cs, and Configuration contains the
     * EntityConfiguration classes. 
     */
    public class Country
    {
        //[Key]
        //[Column("country_id")]
        public string CountryId { get; set; }
        //[Column("country_name")]
        public string CountryName { get; set; }
        //[Column("region_id")]
        public int RegionId { get; set; }

    }
}
