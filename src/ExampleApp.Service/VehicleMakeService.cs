using ExampleApp.Service.Common;
using ExampleApp.Repository.Common;
using ExampleApp.Model.Common;
using ExampleApp.Common;

namespace ExampleApp.Service;
public class VehicleMakeService : IVehicleMakeService
{

    private readonly IVehicleMakeRepository _repository = null!;

    public VehicleMakeService(IVehicleMakeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<IVehicleMake>> GetVehicles(QueryModifier queryModifier) 
    {
        return await _repository.GetVehicles(queryModifier);
    }
    
    public async Task<IVehicleMake?> GetVehicleById(int id)
    {
        return await _repository.GetVehicleById(id);
    }

    public async Task<IVehicleMake?> GetVehicleByName(string name)
    {
        return await _repository.GetVehicleByName(name);
    }

    public async Task<bool> CreateVehicle(IVehicleMake newVehicle) 
    {
       
        var existingVehicle = await this.GetVehicleByName(newVehicle.Name);
        
        if (existingVehicle != null) 
        {
            return false;
        }
        
        return await _repository.CreateVehicle(newVehicle);
    }

    public async Task<bool> UpdateVehicle(int id, IVehicleMake newVehicle)
    {

        var oldVehicle = await this.GetVehicleById(id);

        if (oldVehicle == null) 
        {
            return false;
        }

        var existingVehicle = await GetVehicleByName(newVehicle.Name);

        if (existingVehicle == null)
        {
            return await _repository.UpdateVehicle(newVehicle, oldVehicle);
        } else
        {
            return false;
        }
        
    }

    public async Task<bool> DeleteVehicle(int id)
    {
        var vehicle = await this.GetVehicleById(id);
        
        if (vehicle == null) 
        {
            return false;
        }
        
        return await _repository.DeleteVehicle(vehicle);
    }
  
}
