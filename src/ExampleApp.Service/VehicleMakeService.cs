using ExampleApp.Service.Common;
using ExampleApp.Repository.Common;
using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;

namespace ExampleApp.Service;

public class VehicleMakeService : IVehicleMakeService
{
    private readonly IVehicleMakeRepository _repository = null!;
    public VehicleMakeService(IVehicleMakeRepository repository)
    {
        _repository = repository;
    }
    public async Task<int> CountVehicles()
    {
        return await _repository.CountVehicles();
    }
    public async Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems<VehicleMakeEntity> filterItems, PaginateItems<VehicleMakeEntity> paginateItems) 
    {
        return await _repository.GetVehicles(sortItems, filterItems, paginateItems);
    }
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return await _repository.GetVehicleById(id);
    }
    public async Task<VehicleMake?> GetVehicleByFilter(FilterItems<VehicleMakeEntity> filterItems)
    {
        return await _repository.GetVehicleByFilter(filterItems);
    }
    public async Task<bool> CreateVehicle(VehicleMake newVehicle) 
    {        
        var filterItems = new FilterItems<VehicleMakeEntity>(newVehicle.Name);
        if (await _repository.CheckVehicleByFilter(filterItems)) 
        {
            return false;
        }

        await _repository.CreateVehicle(newVehicle);
        return true;
    }
    public async Task<bool> UpdateVehicle(int id, VehicleMake newVehicle)
    {
        if (!await _repository.CheckVehicleById(id)) 
        {
            return false;
        }

        var filterItems = new FilterItems<VehicleMakeEntity>(newVehicle.Name);

        if (await _repository.CheckVehicleByFilter(filterItems))
        {
            VehicleMake? oldVehicle = await _repository.GetVehicleByFilter(filterItems);
            if (oldVehicle?.Id != id)
            {
                return false;
            }
            
        } 
        newVehicle.Id = id;
        await _repository.UpdateVehicle(newVehicle);
        return true;
    }
    public async Task<bool> DeleteVehicle(int id)
    {     
        if (!await _repository.CheckVehicleById(id)) 
        {
            return false;
        }

        VehicleMake vehicle = new VehicleMake();
        vehicle.Id = id;
    
        await _repository.DeleteVehicle(vehicle);
        return true;
    }
}
