using Microsoft.EntityFrameworkCore;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Entities;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IRepository;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Interfaces.IServices;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Context;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Data.Repositories;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Api.Services;
using ParquimetroDSINAPI.ParquimetroDSINAPI.Business.Services.Pricing;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BaseContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IParkingRepository, ParkingRepository>();
builder.Services.AddScoped<IParkingAreaRepository, ParkingAreaRepository>();

builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IParkingService, ParkingService>();
builder.Services.AddScoped<IParkingAreaService, ParkingAreaService>();

builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddScoped<IPricingStrategy, CarPricingStrategy>();
builder.Services.AddScoped<IPricingStrategy, MotorcyclePricingStrategy>();
builder.Services.AddScoped<IPricingStrategy, VanPricingStrategy>();

// builder.Services.AddScoped<IMapService, GoogleMapService>(); // Descomente quando criar
// builder.Services.AddHttpClient<IMapService, GoogleMapService>(client =>
// {
//     client.BaseAddress = new Uri("https://maps.googleapis.com");
// });



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();