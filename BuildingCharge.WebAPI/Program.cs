using BuildingCharge.Core.Application.DTOs.Charges;
using BuildingCharge.Core.Application.Interfaces;
using BuildingCharge.Core.Application.Mappings;
using BuildingCharge.Core.Application.Services;
using BuildingCharge.Infrastructure.Persistence;
using BuildingCharge.Infrastructure.Repositories;
using BuildingCharge.WebAPI.SwaggerExamples.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// SQL Server ==
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql =>
        {
            sql.MigrationsAssembly("BuildingCharge.Infrastructure");
            sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
});

//builder.Services.AddAutoMapper(typeof(ChargeProfile).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfileMarker).Assembly);


builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IChargeRepository, ChargeRepository>();
//builder.Services.AddScoped<IShareRepository, ShareRepository>();
builder.Services.AddScoped<IChargeCalculator, ChargeCalculator>();
builder.Services.AddScoped<IChargeService, ChargeService>();
builder.Services.AddScoped<IUnitChargeShareRepository, UnitChargeShareRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BuildingCharge API",
        Version = "v1",
        Description = "API for managing building charges"
    });

    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<WaterChargeExample>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    //c.SwaggerEndpoint("v1/swagger.json", "BuildingCharge API v1");
    //    //c.RoutePrefix = "swagger"; // UI at /swagger/index.html
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuildingCharge API v1");
    //    c.RoutePrefix = "swagger";
    //});
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuildingCharge API v1");
        c.RoutePrefix = "swagger"; // UI at /swagger/index.html
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
