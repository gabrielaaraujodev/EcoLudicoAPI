using EcoLudicoAPI.Context;
using EcoLudicoAPI.Repositories;
using EcoLudicoAPI.Repositories.UnitOfWork;
using EcoLudicoAPI.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerFileOperationFilter>();
});

// ----------------------------------
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
//----------------------------------

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//----------------------------------

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//----------------------------------

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        policy => policy.WithOrigins("http://localhost:5173") 
                        .AllowAnyMethod() 
                        .AllowAnyHeader()); 
});
// ---------------------------------

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseCors("AllowMyOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();