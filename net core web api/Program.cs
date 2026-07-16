
using Microsoft.EntityFrameworkCore;
using MyApp.API.Data.Context;

namespace MyApp.API
{
    public class Program
    {
        public static void Main(string[] args)
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
            var connectionString = builder.Configuration.GetConnectionString("HR");
            builder.Services.AddDbContext<HRDbContext>(options => options.UseSqlServer(connectionString));
            
            /*
            builder.Services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
            });

            //builder.Configuration.
            */
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();   // Add by me
                app.UseSwaggerUI(); // Add by me, This enables the web UI
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
