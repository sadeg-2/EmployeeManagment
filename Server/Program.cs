using System.Text;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
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

builder.Services.AddScoped<IGenericRepositoryInterface<GeneralDepartment>, GeneralDepartmentRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Department>, DepartmentRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Branch>, BranchRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Country>, CountryRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<City>, CityRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Town>, TownRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Employee>, EmployeeRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Doctor>, DoctorRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Sanction>, SanctionRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<SanctionType>, SanctionTypeRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<Vacation>, VacationRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<VacationType>, VacationTypeRepository>();

builder.Services.AddScoped<IGenericRepositoryInterface<OverTime>, OverTimeRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<OverTimeType>, OverTimeTypeRepository>();

builder.Services.AddScoped<IImageService, ImageRepository>();



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
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();


app.MapPost("/upload", async (IFormFile file, IImageService imageService) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest("No file uploaded.");

    try
    {
        var imageUrl = await imageService.SaveImageAsync(file);
        return Results.Ok(new { url = imageUrl });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
})
.WithName("UploadImage")
.Accepts<IFormFile>("multipart/form-data")
.Produces(200).DisableAntiforgery();
app.Run();
