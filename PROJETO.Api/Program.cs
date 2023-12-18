using PROJETO.Infra.Database;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Text;
using Microsoft.EntityFrameworkCore;
using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCase.Abstractions.Auth;
using PROJETO.Domain.UseCase.Implementations.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("SqlServerConnectionString")
        )
);

builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

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
