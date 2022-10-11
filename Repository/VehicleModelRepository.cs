using Microsoft.EntityFrameworkCore;
using ExampleApp.Models;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.Helpers.Sort;
using ExampleApp.Data;
namespace ExampleApp.Repository;

public interface IVehicleModelRepository 
{
    public Task<List<VehicleModel>> GetVehicleModels(SortItems sortItems, FilterItems filterItems, PaginateItems<VehicleModel> paginateItems);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<VehicleModel?> GetVehicleModelByName(string name);
    public Task<int> CreateVehicleModel(VehicleModel vehicle);
    public Task<int> UpdateVehicleModel(VehicleModel newModel, VehicleModel oldModel);
    public Task<int> DeleteVehicleModel(VehicleModel model);
}
public class VehicleModelRepository : IVehicleModelRepository
{
  private readonly ExampleAppContext _context = null!;

    public VehicleModelRepository(ExampleAppContext context)
    {
        _context = context;
    }
    
    public async Task<List<VehicleModel>> GetVehicleModels(SortItems sortItems, FilterItems filterItems, PaginateItems<VehicleModel> paginateItems)
    {
        var models = _context.VehicleModel.AsQueryable();
        if (sortItems != null)
        {
            models = sortItems.sort(models);
        }
        if (filterItems != null)
        {
            models = filterItems.filter(models);
        }
        if (paginateItems != null)
        {
            models = await paginateItems.paginate(models);
        }
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try 
        {
            return await models.ToListAsync<VehicleModel>();
        }
        catch 
        {
            return new List<VehicleModel>();
        } 
    }
    
    public async Task<VehicleModel?> GetVehicleModelByName(string name)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try
        {
            return await _context.VehicleModel.Where(m => m.Name == name).FirstOrDefaultAsync();
        }
        catch
        {
            return null;
        }
    }
    
    public async Task<VehicleModel?> GetVehicleModelById(int id)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try
        {
            return await _context.VehicleModel.FindAsync(id);
        }
        catch
        {
            return null;
        }    
    }

    public async Task<int> CreateVehicleModel(VehicleModel vehicleModel) 
    {
        _context.VehicleModel.Add(vehicleModel);
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
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

    public async Task<int> UpdateVehicleModel(VehicleModel newModel, VehicleModel oldModel)
    {
        
        oldModel.MakeId = newModel.MakeId;
        oldModel.Abbrv = newModel.Abbrv;
        oldModel.Name = newModel.Name;
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
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

    public async Task<int> DeleteVehicleModel(VehicleModel model)
    {
        _context.VehicleModel.Remove(model);
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
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