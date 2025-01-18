using ECommerce.Application.Features.Auth;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ECommerce.Application.Interfaces;
using ECommerce.Infrastructure.Security;
using ECommerce.Persistence.DataContext;
using ECommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ECommerce.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Configure Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 2) Configure Services and Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register Infrastructure Services
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

// Register Application Handlers
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<SendVerificationEmailHandler>();
builder.Services.AddScoped<VerifyEmailHandler>();

// Register Validators
builder.Services.AddScoped<IValidator<RegisterRequest>, PasswordValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, EmailValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, UsernameValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, FullNameValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, DateOfBirthValidator>();

// 3) Configure Authentication and JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

// 4) Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()  // Allows all origins. Adjust as needed.
              .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, OPTIONS, etc.).
              .AllowAnyHeader(); // Allows all headers.
    });
});

// 5) Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Enable CORS
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
