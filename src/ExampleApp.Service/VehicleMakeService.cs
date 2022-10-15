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
        VehicleMake? existingVehicle = await this.GetVehicleByName(newVehicle.Name);
        
        if (existingVehicle != null) 
        {
            return false;
        }

        await _repository.CreateVehicle(newVehicle);
        return true;
    }
    public async Task<bool> UpdateVehicle(int id, VehicleMake newVehicle)
    {
        VehicleMake? oldVehicle = await this.GetVehicleById(id);

        if (oldVehicle == null) 
        {
            return false;
        }

        VehicleMake? existingVehicle = await GetVehicleByName(newVehicle.Name);

        if (existingVehicle != null)
        {
            await _repository.UpdateVehicle(newVehicle, oldVehicle);
            return true;
        } else
        {
            return false;
        }
    }
    public async Task<bool> DeleteVehicle(int id)
    {
        VehicleMake? vehicle = await this.GetVehicleById(id);
        
        if (vehicle == null) 
        {
            return false;
        }
        
        await _repository.DeleteVehicle(vehicle);
        return true;
    }
}
