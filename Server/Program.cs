using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Library.Data;
using Server.Library.Helpers;
using Server.Library.Repositories.Contracts;
using Server.Library.Repositories.Implementaions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.Configure<JWTSection>(builder.Configuration.GetSection("JWTSection"));

var jwtSection = builder.Configuration.GetSection(nameof(JWTSection)).Get<JWTSection>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserAccount,UserAccountRepository>();

builder.Services.AddAuthentication(
    options => 
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(
    options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer= jwtSection!.Issuer,
            ValidAudience = jwtSection!.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!)),
            ClockSkew = TimeSpan.Zero
        };
        //options.Events = new JwtBearerEvents
        //{
        //    OnAuthenticationFailed = context =>
        //    {
        //        Console.WriteLine("Authentication failed: " + context.Exception.Message);
        //        return Task.CompletedTask;
        //    },
        //    OnTokenValidated = context =>
        //    {
        //        Console.WriteLine("Token validated successfully.");
        //        return Task.CompletedTask;
        //    }
        //};
    });

builder.Services.AddCors(
options => {

    options.AddPolicy("AllowBlazorWasm",
        builder => builder
        .WithOrigins("https://localhost:7230", "http://localhost:5145")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
