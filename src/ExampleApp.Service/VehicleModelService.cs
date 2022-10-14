using ExampleApp.Service.Common;
using ExampleApp.Repository.Common;
using ExampleApp.Common;
using ExampleApp.Model.Common;

namespace ExampleApp.Service;

public class VehicleModelService : IVehicleModelService
{
    private readonly IVehicleModelRepository _repository = null!;
    private readonly IVehicleMakeService _vehicleService = null!;


    public VehicleModelService(IVehicleModelRepository repository, IVehicleMakeService service)
    {
        _repository = repository;
        _vehicleService = service;
    }

    public async Task<List<IVehicleModel>> GetVehiclesModels(QueryModifier queryModifier) 
    {
        return await _repository.GetVehiclesModels(queryModifier);
    }
    
    public async Task<IVehicleModel?> GetVehicleModelById(int id)
    {
        return await _repository.GetVehicleModelById(id);
    }

    public async Task<IVehicleModel?> GetVehicleModelByName(string name)
    {
        return await _repository.GetVehicleModelByName(name);
    }

    public async Task<bool> CreateVehicleModel(IVehicleModel newVehicleModel) 
    {

        var existingModel = await this.GetVehicleModelByName(newVehicleModel.Name);

        if (existingModel != null) 
        {
            return false;
        }

        var existingVehicle = await _vehicleService.GetVehicleById(newVehicleModel.MakeId);

        if (existingVehicle == null) 
        {
            return false;
        }
        
        return await _repository.CreateVehicleModel(newVehicleModel);
    }

    public async Task<bool> UpdateVehicleModel(int id, IVehicleModel newVehicleModel)
    {
        var oldModel = await this.GetVehicleModelById(id);

        if (oldModel == null) 
        {
            return false;
        }

        var existingVehicle = await _vehicleService.GetVehicleById(newVehicleModel.MakeId);

        if (existingVehicle == null)
        {
            return false;
        }

        var existingModel = await this.GetVehicleModelByName(newVehicleModel.Name);

        if (existingModel != null && id != existingModel.Id)
        {
            return false;
        }
        
        return await _repository.UpdateVehicleModel(newVehicleModel, oldModel);
    }

    public async Task<bool> DeleteVehicleModel(int id)
    {
        var existingModel = await this.GetVehicleModelById(id);

        if (existingModel == null) 
        {
            return false;
        }
        
        return await _repository.DeleteVehicleModel(existingModel);
    }
  
}