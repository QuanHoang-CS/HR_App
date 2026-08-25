using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyApp.API.Models.Identity;
using MyApp.API.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.API.Services
{
    public class JwtService : IJwtService
    {
        // TODO: Implement this class
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager) 
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser user)
        {

            // Get user's role
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("EmployeeId", user.EmployeeId.ToString())
            };

            // Adding roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role) );
            }

            var jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new InvalidOperationException("Jwt key is null");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
