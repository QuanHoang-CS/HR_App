using MyApp.API.Models.Identity;

namespace MyApp.API.Repositories
{
    public interface IJwtService
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }
}
