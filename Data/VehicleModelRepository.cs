using Microsoft.EntityFrameworkCore;
using ExampleApp.Models;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.Helpers.Sort;
namespace ExampleApp.Data;

public interface IVehicleModelRepository 
{
    public Task<List<VehicleModel>> GetVehicleModels(SortItems sortItems, FilterItems? filterItems, PaginateItems<VehicleModel>? paginatedItems);
    public Task<VehicleModel?> GetVehicleModelById(int id);
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
    
    public async Task<List<VehicleModel>> GetVehicleModels(SortItems sortItems, FilterItems? filterItems, PaginateItems<VehicleModel>? paginateItems)
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
            models = paginateItems.paginate(models);
        }

        return await models.ToListAsync<VehicleModel>();
    }
    
    
    public async Task<VehicleModel?> GetVehicleModelById(int id)
    {
        return await _context.VehicleModel.FindAsync(id);
    }

    public async Task<int> CreateVehicleModel(VehicleModel vehicleModel) 
    {
        _context.VehicleModel.Add(vehicleModel);
        await _context.SaveChangesAsync();
        return 201;
    }

    public async Task<int> UpdateVehicleModel(VehicleModel newModel, VehicleModel oldModel)
    {
        
        oldModel.MakeId = newModel.MakeId;
        oldModel.Abbrv = newModel.Abbrv;
        oldModel.Name = newModel.Name;

        await _context.SaveChangesAsync();
        
        return 204;
    }

    public async Task<int> DeleteVehicleModel(VehicleModel model)
    {
        _context.VehicleModel.Remove(model);
        await _context.SaveChangesAsync();
        return 204;
    }
  
}