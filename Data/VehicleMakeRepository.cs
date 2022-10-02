using Microsoft.EntityFrameworkCore;
using ExampleApp.Models;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.Helpers.Sort;
namespace ExampleApp.Data;

public interface IVehicleMakeRepository 
{
    public Task<List<VehicleMake>> GetVehicles(SortItems sortItems, FilterItems? filterItems, PaginateItems<VehicleMake>? paginateItems);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<int> CreateVehicle(VehicleMake vehicle);
    public Task<int> UpdateVehicle(VehicleMake newVehicle, VehicleMake oldVehicle);
    public Task<int> DeleteVehicle(VehicleMake vehicle);
}
public class VehicleMakeRepository : IVehicleMakeRepository
{

  private readonly ExampleAppContext _context = null!;

    public VehicleMakeRepository(ExampleAppContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleMake>> GetVehicles(SortItems sortItems, FilterItems? filterItems, PaginateItems<VehicleMake>? paginateItems)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        if (sortItems != null)
        {
            vehicles = sortItems.sort(vehicles);
        }
        if (filterItems != null)
        {
            vehicles = filterItems.filter(vehicles);
        }
        if (paginateItems != null)
        {
            vehicles = paginateItems.paginate(vehicles);
        }

        return await vehicles.ToListAsync<VehicleMake>();
    }
    
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return await _context.VehicleMake.FindAsync(id);
    }

    public async Task<int> CreateVehicle(VehicleMake vehicle) 
    {
        _context.VehicleMake.Add(vehicle);
        await _context.SaveChangesAsync();
        return 201;
    }

    public async Task<int> UpdateVehicle(VehicleMake newVehicle, VehicleMake oldVehicle)
    {
        
        oldVehicle.Abbrv = newVehicle.Abbrv;
        oldVehicle.Name = newVehicle.Name;

        await _context.SaveChangesAsync();
        
        return 204;
    }

    public async Task<int> DeleteVehicle(VehicleMake vehicle)
    {
        _context.VehicleMake.Remove(vehicle);
        await _context.SaveChangesAsync();
        return 204;
    }
  
}