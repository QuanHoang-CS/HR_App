using Microsoft.AspNetCore.Identity;

namespace MyApp.API.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int EmployeeId { get; set; }
    }
}
