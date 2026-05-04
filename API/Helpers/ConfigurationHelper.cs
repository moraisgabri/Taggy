using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Taggy.Application.Services;
using Taggy.Domain.Interfaces;
using Taggy.Infrastructure.Data;
using Taggy.Infrastructure.Repositories;

namespace Taggy.API.Helpers;

class ConfigurationHelper
{
    static public void ConfigureServices(WebApplicationBuilder builder)
    {
        // Conexão com o banco de dados, nesse caso PostgreSQL
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Injeção de dependências
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddControllers();

        // Documentação
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title   = "Taggy API",
                Version = "v1",
                Description = "API de cálculos de emissões e resíduos."
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name         = "Authorization",
                Type         = SecuritySchemeType.Http,
                Scheme       = "bearer",
                BearerFormat = "JWT",
                In           = ParameterLocation.Header,
                Description  = "Informe o token JWT. Exemplo: Bearer {seu_token}"
            });
        });

        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();

        // Adicionando política do CORS.
        // Está bem permissiva pra evitar erros na integração com o front.
        builder.Services.AddCors(options => 
        {
            options.AddPolicy("AllowAll", policy => 
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

    static public void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var jwtConfig = builder.Configuration.GetSection("Jwt");

        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]! ?? "asdfasdf");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer           = true,
                ValidateAudience         = true,
                ValidateLifetime         = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer              = jwtConfig["Issuer"],
                ValidAudience            = jwtConfig["Audience"],
                IssuerSigningKey         = new SymmetricSecurityKey(key),
                ClockSkew                = TimeSpan.Zero
            };
        });
    }

    static public void HandleEnvironment(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Taggy");
            });
        }
    }
}