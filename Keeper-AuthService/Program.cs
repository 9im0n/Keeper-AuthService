using Keeper_AuthService.Services.Implementations;
using Keeper_AuthService.Services.Interfaces;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.Settings;
using Keeper_AuthService.DB;
using Microsoft.EntityFrameworkCore;
using Keeper_AuthService.Repositories.Interfaces;
using Keeper_AuthService.Repositories.Implementations;
using Keeper_AuthService.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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

            // Configuration
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailService"));

            // Auth
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetSection("JwtSettings:issuer").Value,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration.GetSection("JwtSettings:audience").Value,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                            builder.Configuration.GetSection("JwtSettings:Key").Value!
                            )),
                        ValidAlgorithms = new string[] { SecurityAlgorithms.HmacSha256 },
                    };
                });

            //Db
            string host = Environment.GetEnvironmentVariable("DB_HOST")!;
            string port = Environment.GetEnvironmentVariable("DB_PORT")!;
            string db = Environment.GetEnvironmentVariable("DB_NAME")!;
            string user = Environment.GetEnvironmentVariable("DB_USER")!;
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD")!;

            string connection = $"Host={host};Port={port};Database={db};Username={user};Password={password}";
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));

            // Repositories
            builder.Services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
            builder.Services.AddScoped<IPendingActivationsRepository, PendingActivationsRepository>();


            // Services
            builder.Services.AddScoped<IActivationPasswordService, ActivationPasswordService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IDTOMapper, DTOMapper>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IPendingActivationService, PendingActivationService>();
            builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();            

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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
