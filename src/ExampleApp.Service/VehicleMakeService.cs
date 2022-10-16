using ExampleApp.Service.Common;
using ExampleApp.Repository.Common;
using ExampleApp.Model;
using ExampleApp.Common;

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
    public async Task<List<VehicleMake>> GetVehicles(QueryDataSFP queryDataSFP) 
    {
        return await _repository.GetVehicles(queryDataSFP);
    }
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return await _repository.GetVehicleById(id);
    }
    public async Task<VehicleMake?> GetVehicleByName(string name)
    {
        return await _repository.GetVehicleByName(name);
    }
    public async Task<bool> CreateVehicle(VehicleMake newVehicle) 
    {        
        if (await _repository.CheckVehicleByName(newVehicle.Name)) 
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

        if (await _repository.CheckVehicleByName(newVehicle.Name))
        {
            VehicleMake? oldVehicle = await _repository.GetVehicleByName(newVehicle.Name);
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
