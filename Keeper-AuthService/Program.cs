using Keeper_AuthService.Services.Implemitations;
using Keeper_AuthService.Services.Interfaces;
using Keeper_AuthService.Controllers;
using Keeper_AuthService.DB;
using Microsoft.EntityFrameworkCore;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Repositories.Interfaces;
using Keeper_AuthService.Repositories.Implemintations;
using Keeper_UserService.Repositories.Implemintations;
using Keeper_AuthService.Services.Implemintations;

namespace Keeper_AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddHttpClient<IHttpClientService, HttpClientService>();
            builder.Services.Configure<ApiUrls>(builder.Configuration.GetSection("ApiUrls"));

            // Db
            string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));

            // Repos

            builder.Services.AddScoped<IActivationPasswordsRepository, ActivationPasswordsRepository>();

            // Services

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IActivationPasswordsService, ActivationPasswordsService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
