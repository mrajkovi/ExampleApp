using Microsoft.EntityFrameworkCore;
using ExampleApp.Models;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.Helpers.Sort;
using ExampleApp.Data;
namespace ExampleApp.Repository;

public interface IVehicleMakeRepository 
{
    public Task<List<VehicleMake>> GetVehicles(SortItems sortItems, FilterItems filterItems, PaginateItems<VehicleMake> paginateItems);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<VehicleMake?> GetVehicleByName(string name);
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

    public async Task<List<VehicleMake>> GetVehicles(SortItems sortItems, FilterItems filterItems, PaginateItems<VehicleMake> paginateItems)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        vehicles = sortItems.sort(vehicles);
        vehicles = filterItems.filter(vehicles);
        vehicles = await paginateItems.paginate(vehicles);
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            return await vehicles.ToListAsync();
        }
        catch
        {
            return new List<VehicleMake>();
        }
        
    }
    
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            return await _context.VehicleMake.FindAsync(id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<VehicleMake?> GetVehicleByName(string name)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            return await _context.VehicleMake.Where(v => v.Name == name).FirstOrDefaultAsync();
        }
        catch
        {
            return null;
        }
    }

    public async Task<int> CreateVehicle(VehicleMake vehicle) 
    {
        _context.VehicleMake.Add(vehicle);
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            await _context.SaveChangesAsync();
            return 201;
        }
        catch
        {
            return 500;
        }
    }

    public async Task<int> UpdateVehicle(VehicleMake newVehicle, VehicleMake oldVehicle)
    {
        
        oldVehicle.Abbrv = newVehicle.Abbrv;
        oldVehicle.Name = newVehicle.Name;

        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            await _context.SaveChangesAsync();
            return 204;
        }
        catch
        {
            return 500;
        }
    }

    public async Task<int> DeleteVehicle(VehicleMake vehicle)
    {
        _context.VehicleMake.Remove(vehicle);
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            await _context.SaveChangesAsync();
            return 204;
        }
        catch
        {
            return 500;
        }
    }
  
}