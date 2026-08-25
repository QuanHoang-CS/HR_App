using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.API.Data.Context;
using MyApp.API.Models.DTO;
using MyApp.API.Models.Identity;
using MyApp.API.Repositories;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationIdentityDbContext _dbContext;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<ApplicationUser> userManager, ApplicationIdentityDbContext dbContext, IJwtService jwtService)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _jwtService = jwtService;
        }


        // TODO: Split the creating of role to another API endpoint
        // POST: /api/Auth/Register
        [HttpPost("Register", Name = "Create User")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var appUser = new ApplicationUser
            {
                UserName = registerRequest.Username,
                EmployeeId = registerRequest.EmployeeId,
                Email = registerRequest.Username
            };

            var result = await _userManager.CreateAsync(appUser, registerRequest.Password);

            // Catch password errors
            if ( !result.Succeeded )
            {
                var errorDescriptions = result.Errors.Select(e => e.Description);
                return BadRequest(new {message = "Something went wrong!!", errorDescriptions});
            }
            else
            {
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                    result = await _userManager.AddToRoleAsync(appUser, registerRequest.Roles);
            }
        
            if( !result.Succeeded)
            {
                var errorDescriptions = result.Errors.Select(e => e.Description);
                return BadRequest(new { message = "Something went wrong!!", errorDescriptions });
            }
            
            return Ok("User was registered!");
        }


        // POST: api/auth/login
        // TODO: Implementing Login
        [HttpPost("Login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.UserName);

            if(user == null)
            {
                return Unauthorized("Username or password incorrect");
            }
            
            var passwordCorrect = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            if( !passwordCorrect )
            {
                return Unauthorized("Username or password incorrect");
            }
            
            var token = _jwtService.CreateTokenAsync(user);

            return Ok(new { token });
        }
    }
    
}
