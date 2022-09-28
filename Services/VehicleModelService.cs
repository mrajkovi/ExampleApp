using Microsoft.EntityFrameworkCore;
using ExampleApp.Data;
using ExampleApp.Models;
using ExampleApp.Interfaces.Services.Vehicle;
namespace ExampleApp.Services;

public class VehicleModelService : IVehicleServiceInterface<VehicleModel>
{
  private readonly ExampleAppContext _context = null!;

    public VehicleModelService(ExampleAppContext context)
    {
        _context = context;
    }

    public async Task<List<VehicleModel>> GetAll(string? sortOrder, string? searchString, string? pageNumber, string? pageSize) 
    {
        var vehicleModels = _context.VehicleModel.AsQueryable();
        if (!String.IsNullOrEmpty(searchString))
        {
            vehicleModels = vehicleModels.Where(v => v.Name.Contains(searchString) || v.Abbrv.Contains(searchString));
        }
        if (Int32.TryParse(searchString, out int vehicleId))
        {
            vehicleModels = vehicleModels.Where(v => v.MakeId == vehicleId);
        }
        switch (sortOrder)
        {   
            case "name_asc":
                vehicleModels = vehicleModels.OrderBy(v => v.Name);
                break;
            case "name_desc":
                vehicleModels = vehicleModels.OrderByDescending(v => v.Name);
                break;
            case "abbrv_asc":
                vehicleModels = vehicleModels.OrderBy(v => v.Abbrv);
                break;
            case "abbrv_desc":
                vehicleModels = vehicleModels.OrderByDescending(v => v.Abbrv);
                break;
            default:
                break;

        }

        if (!String.IsNullOrEmpty(pageNumber) && !String.IsNullOrEmpty(pageSize))
        {
            return await IVehicleServiceInterface<VehicleModel>.PaginatedList<VehicleModel>.CreateAsync(vehicleModels, Int32.Parse(pageNumber), Int32.Parse(pageSize));
        }

        return await vehicleModels.ToListAsync<VehicleModel>();
    }
    
    public async Task<VehicleModel?> GetById(int id)
    {
        return await _context.VehicleModel.FindAsync(id);
    }

    public async Task<int> Create(VehicleModel vehicleModel) 
    {
        var existingModelVehicle = await _context.VehicleModel.FindAsync(vehicleModel.Id);
        if (existingModelVehicle != null) 
        {
            return 404;
        }
        var existingVehicle = await _context.VehicleMake.FindAsync(vehicleModel.MakeId);
        if (existingVehicle != null)
        {
            _context.VehicleModel.Add(vehicleModel);
            await _context.SaveChangesAsync();
            return 201;
        } else 
        {
            return 400;
        }
    }

    public async Task<int> Update(int id, VehicleModel vehicleModel)
    {
        if (id != vehicleModel.Id) {
            return 400;
        }
        
        var existingVehicleModel = await _context.VehicleModel.FindAsync(id);

        if (existingVehicleModel == null) {
            return 404;
        }

        var existingVehicle = await _context.VehicleMake.FindAsync(vehicleModel.MakeId);

        if (existingVehicle == null) {
            return 400;
        }
        existingVehicleModel.MakeId = vehicleModel.MakeId;
        existingVehicleModel.Abbrv = vehicleModel.Abbrv;
        existingVehicleModel.Name = vehicleModel.Name;

        await _context.SaveChangesAsync();
        
        return 204;
    }

    public async Task<int> Delete(int id)
    {
        var vehicleModel = await _context.VehicleModel.FindAsync(id);
        if (vehicleModel == null) 
        {
            return 404;
        }
        
        _context.VehicleModel.Remove(vehicleModel);
        await _context.SaveChangesAsync();

        return 204;
    }
  
}