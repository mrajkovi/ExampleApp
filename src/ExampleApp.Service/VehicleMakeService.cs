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
    public async Task<int> CountVehicles(FilterItems<VehicleMakeEntity> filterVehicles)
    {
        return await _repository.CountVehicles(filterVehicles);
    }
    public async Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems<VehicleMakeEntity> filterVehicles, PaginateItems<VehicleMakeEntity> paginateItems) 
    {
        return await _repository.GetVehicles(sortItems, filterVehicles, paginateItems);
    }
    public async Task<VehicleMake?> GetVehicle(FilterItems<VehicleMakeEntity> filterVehicles)
    {
        return await _repository.GetVehicle(filterVehicles);
    }
    public async Task<bool> CreateVehicle(VehicleMake newVehicle) 
    {        
        var filterVehiclesByName = new FilterItems<VehicleMakeEntity>(newVehicle.Name, "name");

        if (await _repository.CheckVehicle(filterVehiclesByName)) 
        {
            return false;
        }

        await _repository.CreateVehicle(newVehicle);
        return true;
    }
    public async Task<bool> UpdateVehicle(FilterItems<VehicleMakeEntity> filterVehiclesById, VehicleMake newVehicle)
    {
        if (!await _repository.CheckVehicle(filterVehiclesById)) 
        {
            return false;
        }

        var filterVehiclesByName = new FilterItems<VehicleMakeEntity>(newVehicle.Name, "name", true);

        var newVehicleId = Int32.Parse(filterVehiclesById.FilterString);

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
    public async Task<bool> DeleteVehicle(FilterItems<VehicleMakeEntity> filterVehiclesById)
    {     
        if (!await _repository.CheckVehicle(filterVehiclesById)) 
        {
            return false;
        }

        VehicleMake vehicle = new VehicleMake();
        vehicle.Id = Int32.Parse(filterVehiclesById.FilterString);
    
        await _repository.DeleteVehicle(vehicle);

        return true;
    }
}
