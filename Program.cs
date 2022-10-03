using ExampleApp.Data;
using ExampleApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ExampleAppContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("ExampleAppContext")));

        // Add services to the container.
        builder.Services.AddScoped<IVehicleMakeRepository, VehicleMakeRepository>();
        builder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
        builder.Services.AddScoped<IVehicleMakeService, VehicleMakeService>();
        builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment()) {}

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.MapControllers();

        app.Run();
    }
}