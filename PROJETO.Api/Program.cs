using PROJETO.Infra.Identity;
using PROJETO.Infra.Database;
using PROJETO.Domain.Services;
using PROJETO.Domain.Repository.Auth;
using PROJETO.Infra.Interfaces.Services;
using PROJETO.Domain.Interfaces.Repository.Auth;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Text;
using PROJETO.DTO.Mapper.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("SqlServerConnectionString")
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
                Encoding.UTF8.GetBytes("colocarjwt")
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

builder.Services.AddSingleton<RegisterMapper>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();

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
