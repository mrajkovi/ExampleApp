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
    public async Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems filterVehicles, PaginateItems<VehicleMakeEntity> paginateItems) 
    {
        return await _repository.GetVehicles(sortItems, filterVehicles, paginateItems);
    }
    public async Task<VehicleMake?> GetVehicle(FilterItems filterVehicles)
    {
        return await _repository.GetVehicle(filterVehicles);
    }
    public async Task<bool> CreateVehicle(VehicleMake newVehicle) 
    {        
        var filterVehiclesByName = new FilterItems(newVehicle.Name, "name");
        if (await _repository.CheckVehicle(filterVehiclesByName)) 
        {
            return false;
        }

        await _repository.CreateVehicle(newVehicle);
        return true;
    }
    public async Task<bool> UpdateVehicle(FilterItems filterVehiclesById, VehicleMake newVehicle)
    {
        var newVehicleId = Int32.Parse(filterVehiclesById.FilterString);
        if (!await _repository.CheckVehicle(filterVehiclesById)) 
        {
            return false;
        }

        var filterVehiclesByName = new FilterItems(newVehicle.Name, "name", true);

        if (await _repository.CheckVehicle(filterVehiclesByName))
        {
            VehicleMake? oldVehicle = await _repository.GetVehicle(filterVehiclesById);
            if (oldVehicle?.Id != newVehicleId)
            {
                return false;
            }
            
        } 
        newVehicle.Id = newVehicleId;
        await _repository.UpdateVehicle(newVehicle);
        return true;
    }
    public async Task<bool> DeleteVehicle(FilterItems filterVehicles)
    {     
        if (!await _repository.CheckVehicle(filterVehicles)) 
        {
            return false;
        }

        VehicleMake vehicle = new VehicleMake();
        vehicle.Id = Int32.Parse(filterVehicles.FilterString);
    
        await _repository.DeleteVehicle(vehicle);
        return true;
    }
}
