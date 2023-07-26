using PROJETO.Api.Swagger;
using PROJETO.DTO.Automapper;
using PROJETO.Infra.Database;
using PROJETO.Domain.Services;
using PROJETO.Domain.Identity;
using PROJETO.Domain.Interfaces;
using PROJETO.Domain.Repository;
using PROJETO.Domain.Interfaces.Services;
using PROJETO.Domain.Interfaces.Repository;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var keyVaultUrl = builder.Configuration.GetValue<string>("AzureKeyVaultUrl");
var secretsClient = new SecretClient(new Uri(keyVaultUrl!), new DefaultAzureCredential());

builder.Services.AddDbContext<MyDbContext>(
    options =>
        options.UseSqlServer(
            secretsClient.GetSecret("Projetoddd-ConnectionString").Value.Value
        )
);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretsClient.GetSecret("Jwt--Key").Value.Value)
            )
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        PolicyRules.AdminPolicyName,
        p => p.RequireClaim(PolicyRules.ClaimTitle, PolicyRules.AdminClaimName)
    );
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        PolicyRules.UserPolicyName,
        p => p.RequireClaim(PolicyRules.ClaimTitle, PolicyRules.UserClaimName)
    );
});

builder.Services.AddAutoMapper(typeof(MyAutoMapper));
builder.Services.AddTransient<
    IConfigureOptions<SwaggerGenOptions>,
    ConfigureSwaggerOptions
>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
