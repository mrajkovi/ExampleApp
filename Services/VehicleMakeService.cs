using Microsoft.EntityFrameworkCore;
using ExampleApp.Data;
using ExampleApp.Models;
using ExampleApp.Interfaces.Services.Vehicle;
namespace ExampleApp.Services;

public class VehicleMakeService : IVehicleServiceInterface<VehicleMake>
{
  private readonly ExampleAppContext _context = null!;

    public VehicleMakeService(ExampleAppContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleMake>> GetAll(string? sortOrder, string? searchString, string? pageNumber, string? pageSize) 
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        if (!String.IsNullOrEmpty(searchString))
        {
            vehicles = vehicles.Where(v => v.Name.Contains(searchString) || v.Abbrv.Contains(searchString));
        }
        switch (sortOrder)
        {   
            case "name_asc":
                vehicles = vehicles.OrderBy(v => v.Name);
                break;
            case "name_desc":
                vehicles = vehicles.OrderByDescending(v => v.Name);
                break;
            case "abbrv_asc":
                vehicles = vehicles.OrderBy(v => v.Abbrv);
                break;
            case "abbrv_desc":
                vehicles = vehicles.OrderByDescending(v => v.Abbrv);
                break;
            default:
                break;

        }

        if (!String.IsNullOrEmpty(pageNumber) && !String.IsNullOrEmpty(pageSize))
        {
            return await IVehicleServiceInterface<VehicleMake>.PaginatedList<VehicleMake>.CreateAsync(vehicles, Int32.Parse(pageNumber), Int32.Parse(pageSize));
        }

        return await vehicles.ToListAsync<VehicleMake>();
    }
    
    public async Task<VehicleMake?> GetById(int id)
    {
        return await _context.VehicleMake.FindAsync(id);
    }

    public async Task<int> Create(VehicleMake vehicle) 
    {
        var existingVehicle = await _context.VehicleMake.FindAsync(vehicle.Id);
        if (existingVehicle != null) 
        {
            return 404;
        }
        _context.VehicleMake.Add(vehicle);
        await _context.SaveChangesAsync();
        return 201;
    }

    public async Task<int> Update(int id, VehicleMake vehicle)
    {
        if (id != vehicle.Id) {
            return 400;
        }
        
        var foundVehicle = await _context.VehicleMake.FindAsync(id);

        if (foundVehicle == null) {
            return 404;
        }

        foundVehicle.Abbrv = vehicle.Abbrv;
        foundVehicle.Name = vehicle.Name;

        await _context.SaveChangesAsync();
        
        return 204;
    }

    public async Task<int> Delete(int id)
    {
        var vehicle = await _context.VehicleMake.FindAsync(id);
        if (vehicle == null) 
        {
            return 404;
        }
        
        _context.VehicleMake.Remove(vehicle);
        await _context.SaveChangesAsync();

        return 204;
    }
  
}