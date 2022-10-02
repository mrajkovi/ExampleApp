using ExampleApp.Models;
using ExampleApp.Data;
using ExampleApp.Helpers.Sort;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
namespace ExampleApp.Services;

public interface IVehicleMakeService 
{
    public Task<List<VehicleMake>> GetVehicles(string? sortOrder, string? searchString, string? pageNumber, string? pageSize);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<int> CreateVehicle(VehicleMake vehicle);
    public Task<int> UpdateVehicle(int id, VehicleMake vehicle);
    public Task<int> DeleteVehicle(int id);
}
public class VehicleMakeService : IVehicleMakeService
{

    private readonly IVehicleMakeRepository _repository = null!;

    public VehicleMakeService(IVehicleMakeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<VehicleMake>> GetVehicles(string? sortOrder, string? searchString, string? pageNumber, string? pageSize) 
    {
        var sortVehicles = new SortItems(sortOrder);
        FilterItems? filterVehicles = null;
        PaginateItems<VehicleMake>? paginateVehicles = null;
        if (searchString != null) 
        {
            filterVehicles = new FilterItems(searchString);
        }
        if (!String.IsNullOrEmpty(pageNumber) && !String.IsNullOrEmpty(pageSize))
        {
            paginateVehicles = new PaginateItems<VehicleMake>(Int32.Parse(pageNumber), Int32.Parse(pageSize));
        }
        return await _repository.GetVehicles(sortVehicles, filterVehicles, paginateVehicles);
    }
    
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return await _repository.GetVehicleById(id);
    }

    public async Task<int> CreateVehicle(VehicleMake vehicle) 
    {
        var existingVehicle = await this.GetVehicleById(vehicle.Id);

        if (existingVehicle != null) 
        {
            return 404;
        }
        
        return await _repository.CreateVehicle(vehicle);
    }

    public async Task<int> UpdateVehicle(int id, VehicleMake vehicle)
    {
        if (id != vehicle.Id) {
            return 400;
        }

        var oldvehicle = await this.GetVehicleById(id);

        if (oldvehicle == null) 
        {
            return 404;
        }
        
        return await _repository.UpdateVehicle(vehicle, oldvehicle);
    }

    public async Task<int> DeleteVehicle(int id)
    {
        var vehicle = await this.GetVehicleById(id);
        if (vehicle == null) 
        {
            return 404;
        }
        
        await _repository.DeleteVehicle(vehicle);

        return 204;
    }
  
}