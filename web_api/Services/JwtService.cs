using MyApp.API.Models.Identity;

namespace MyApp.API.Services
{
    public class JwtService : IJwtService
    {
        public async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
