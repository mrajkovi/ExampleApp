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
    public async Task<int> CountVehiclesModels()
    {
        return await _repository.CountVehiclesModels();
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
        if (await _repository.CheckVehicleModelByName(newVehicleModel.Name)) 
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
        if (!await _repository.CheckVehicleModelById(id)) 
        {
            return false;
        }

        VehicleMake? existingVehicle = await _vehicleService.GetVehicleById(newVehicleModel.MakeId);

        if (existingVehicle == null)
        {
            return false;
        }

        if (await _repository.CheckVehicleModelByName(newVehicleModel.Name))
        {
            VehicleModel? oldVehicleModel = await _repository.GetVehicleModelByName(newVehicleModel.Name);
            if (oldVehicleModel?.Id != id)
            {
                return false;
            }
        }
    
        newVehicleModel.Id = id;
        await _repository.UpdateVehicleModel(newVehicleModel);
        return true;
    }
    public async Task<bool> DeleteVehicleModel(int id)
    {
        if (!await _repository.CheckVehicleModelById(id)) 
        {
            return false;
        }
        
        VehicleModel vehicleModel = new VehicleModel();
        vehicleModel.Id = id;
        await _repository.DeleteVehicleModel(vehicleModel);
        return true;
    }
}