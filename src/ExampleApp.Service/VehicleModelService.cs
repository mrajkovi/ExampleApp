using ExampleApp.Service.Common;
using ExampleApp.Repository.Common;
using ExampleApp.Common;
using ExampleApp.Model;

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
    public async Task<List<VehicleModel>> GetVehiclesModels(QueryDataSFP queryDataSFP) 
    {
        return await _repository.GetVehiclesModels(queryDataSFP);
    }
    public async Task<VehicleModel?> GetVehicleModelById(int id)
    {
        return await _repository.GetVehicleModelById(id);
    }
    public async Task<VehicleModel?> GetVehicleModelByName(string name)
    {
        return await _repository.GetVehicleModelByName(name);
    }
    public async Task<bool> CreateVehicleModel(VehicleModel newVehicleModel) 
    {
        VehicleModel? existingModel = await this.GetVehicleModelByName(newVehicleModel.Name);

        if (existingModel != null) 
        {
            return false;
        }

        VehicleMake? existingVehicle = await _vehicleService.GetVehicleById(newVehicleModel.MakeId);

        if (existingVehicle == null) 
        {
            return false;
        }
        
        await _repository.CreateVehicleModel(newVehicleModel);
        return true;
    }
    public async Task<bool> UpdateVehicleModel(int id, VehicleModel newVehicleModel)
    {
        VehicleModel? oldModel = await this.GetVehicleModelById(id);

        if (oldModel == null) 
        {
            return false;
        }

        VehicleMake? existingVehicle = await _vehicleService.GetVehicleById(newVehicleModel.MakeId);

        if (existingVehicle == null)
        {
            return false;
        }

        VehicleModel? existingModel = await this.GetVehicleModelByName(newVehicleModel.Name);

        if (existingModel != null && id != existingModel.Id)
        {
            return false;
        }
        
        await _repository.UpdateVehicleModel(newVehicleModel, oldModel);
        return true;
    }
    public async Task<bool> DeleteVehicleModel(int id)
    {
        VehicleModel? existingModel = await this.GetVehicleModelById(id);

        if (existingModel == null) 
        {
            return false;
        }
        
        await _repository.DeleteVehicleModel(existingModel);
        return true;
    }
}