using MyApp.API.Models.Identity;

namespace MyApp.API.Services
{
    public interface IJwtService
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }
}
