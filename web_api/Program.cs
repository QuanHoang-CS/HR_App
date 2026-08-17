
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApp.API.Data.Context;
using MyApp.API.Data.Identity;
using MyApp.API.Models.Identity;
using MyApp.API.Services;
using System.Text;

namespace MyApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            //builder.Services.AddEndpointsApiExplorer(); // Old methods in previous .NET that's no longer included in ther template.
                                                          // what does it do?


            builder.Services.AddSwaggerGen();   // This line was added by me for Swagger
                                                // Without this line, we will encounter error:
                                                // This error occurs when you have enabled the Swagger middleware in your application's request pipeline,
                                                // but haven't registered the required services in the dependency injection (DI) container.

            // This error occurs when you have enabled
            // the Swagger middleware in your application's
            // request pipeline, but haven't registered
            // the required services in the dependency injection (DI) container. 

            // Add your db to the depecdency injection
            var connectionStringToHR = builder.Configuration.GetConnectionString("HR");
            var connectionStringToRole = builder.Configuration.GetConnectionString("Authentication");

            if (connectionStringToHR == null || connectionStringToRole == null)
                throw new InvalidOperationException("Connection string to HR or HR role not found");

            builder.Services.AddDbContext<HRDbContext>(options => options.UseSqlServer(connectionStringToHR));
            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(connectionStringToRole));
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddIdentityCore<ApplicationUser>()
                            .AddRoles<IdentityRole>()
                            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("HRDb")
                            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 7;
                options.Password.RequiredUniqueChars = 1;
            });   
            /*
            builder.Services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            //builder.Configuration.
            */

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    //
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();       // Add by me
                app.UseSwaggerUI();     // Add by me, This enables the web UI
            }

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await IdentitySeeder.SeedRolesAsync(roleManager);
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();    // Authenticating b4 authorizing
            
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
