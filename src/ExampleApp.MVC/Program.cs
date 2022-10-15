using Microsoft.EntityFrameworkCore;
using ExampleApp.DAL;
using ExampleApp.Repository;
using ExampleApp.Repository.Common;
using ExampleApp.Service;
using ExampleApp.Service.Common;

namespace ExampleApp.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var webApplicationBuilder = WebApplication.CreateBuilder(args);

        // Add database context.
        webApplicationBuilder.Services.AddDbContext<ExampleAppContext>(optionsAction =>
            optionsAction.UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("ExampleAppContext")));

        // Add automapper.
        webApplicationBuilder.Services.AddAutoMapper(configAction => 
        {
            configAction.AddProfile<MappingProfileWebAPI>();
        });

        // Add services to the container.
        webApplicationBuilder.Services.AddScoped<IVehicleMakeRepository, VehicleMakeRepository>();
        webApplicationBuilder.Services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
        webApplicationBuilder.Services.AddScoped<IVehicleMakeService, VehicleMakeService>();
        webApplicationBuilder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
        webApplicationBuilder.Services.AddControllersWithViews();
        webApplicationBuilder.Services.AddRouting(options => options.LowercaseUrls = true);

        var app = webApplicationBuilder.Build();

        if (!app.Environment.IsDevelopment()) {}

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.MapControllers();

        app.Run();
    }
}