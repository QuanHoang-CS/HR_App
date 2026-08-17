using System.ComponentModel.DataAnnotations;

namespace MyApp.API.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
