using BuildingCharge.Core.Application.Interfaces;
using BuildingCharge.Core.Application.Services;
using BuildingCharge.Infrastructure.Persistence;
using BuildingCharge.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IChargeRepository, ChargeRepository>();
//builder.Services.AddScoped<IShareRepository, ShareRepository>();
builder.Services.AddScoped<IChargeCalculator, ChargeCalculator>();
builder.Services.AddScoped<IChargeService, ChargeService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
