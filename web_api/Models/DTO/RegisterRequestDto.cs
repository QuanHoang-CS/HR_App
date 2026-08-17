using System.ComponentModel.DataAnnotations;

namespace MyApp.API.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set;  }
        [Required]
        public string Password { get; set; }
        public string? Roles { get; set; }
        [Required]
        public int EmployeeId {  get; set; }

    }
}
