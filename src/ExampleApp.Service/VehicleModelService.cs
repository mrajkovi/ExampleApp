using ExampleApp.Service.Common;
using ExampleApp.Repository.Common;
using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.DAL;

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
    public async Task<int> CountVehiclesModels(FilterItems<VehicleModelEntity> filterModels)
    {
        return await _repository.CountVehiclesModels(filterModels);
    }
    public async Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems<VehicleModelEntity> filterModels, PaginateItems<VehicleModelEntity> paginateItems) 
    {
        return await _repository.GetVehiclesModels(sortItems, filterModels, paginateItems);
    }

    public async Task<VehicleModel?> GetVehicleModel(FilterItems<VehicleModelEntity> filterModels)
    {
        return await _repository.GetVehicleModel(filterModels);
    }
    public async Task<bool> CreateVehicleModel(VehicleModel newVehicleModel) 
    {
        var filterItemsByName = new FilterItems<VehicleModelEntity>(newVehicleModel.Name, "name", true);
        
        if (await _repository.CheckVehicleModel(filterItemsByName)) 
        {
            return false;
        }

        var filterItemsById = new FilterItems<VehicleMakeEntity>(newVehicleModel.MakeId.ToString(), "id");
        
        VehicleMake? existingVehicle = await _vehicleService.GetVehicle(filterItemsById);

        if (existingVehicle == null) 
        {
            return false;
        }
        
        await _repository.CreateVehicleModel(newVehicleModel);
    
        return true;
    }
    public async Task<bool> UpdateVehicleModel(FilterItems<VehicleModelEntity> filterModelsById, VehicleModel newVehicleModel)
    {
        if (!await _repository.CheckVehicleModel(filterModelsById)) 
        {
            return false;
        }

        var filterVehiclesById = new FilterItems<VehicleMakeEntity>(newVehicleModel.MakeId.ToString(), "id");

        VehicleMake? existingVehicle = await _vehicleService.GetVehicle(filterVehiclesById);

        if (existingVehicle == null)
        {
            return false;
        }

        var filterModelsByName = new FilterItems<VehicleModelEntity>(newVehicleModel.Name, "name", true);
        var vehicleModelId = Int32.Parse(filterModelsById.FilterString);
        
        if (await _repository.CheckVehicleModel(filterModelsByName))
        {
            VehicleModel? oldVehicleModel = await _repository.GetVehicleModel(filterModelsByName);
            if (oldVehicleModel?.Id != vehicleModelId)
            {
                return false;
            }
        }
    
        newVehicleModel.Id = vehicleModelId;

        await _repository.UpdateVehicleModel(newVehicleModel);

        return true;
    }
    public async Task<bool> DeleteVehicleModel(FilterItems<VehicleModelEntity> filterModelsById)
    {
        if (!await _repository.CheckVehicleModel(filterModelsById)) 
        {
            return false;
        }

        var vehicleModelId = Int32.Parse(filterModelsById.FilterString);
        VehicleModel vehicleModel = new VehicleModel();
        vehicleModel.Id = vehicleModelId;

        await _repository.DeleteVehicleModel(vehicleModel);

        return true;
    }
}