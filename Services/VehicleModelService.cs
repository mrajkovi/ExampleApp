using ExampleApp.Models;
using ExampleApp.Data;
using ExampleApp.Helpers.Sort;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
namespace ExampleApp.Services;

public interface IVehicleModelService 
{
    public Task<List<VehicleModel>> GetVehicleModels(string? sortOrder, string? searchString, string? pageNumber, string? pageSize);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<int> CreateVehicleModel(VehicleModel model);
    public Task<int> UpdateVehicleModel(int id, VehicleModel newModel);
    public Task<int> DeleteVehicleModel(int id);
}
public class VehicleModelService : IVehicleModelService
{
    private readonly IVehicleModelRepository _repository = null!;
    private readonly IVehicleMakeService _vehicleService = null!;

    public VehicleModelService(IVehicleModelRepository repository, IVehicleMakeService service)
    {
        _repository = repository;
        _vehicleService = service;
    }

    public async Task<List<VehicleModel>> GetVehicleModels(string? sortOrder, string? searchString, string? pageNumber, string? pageSize) 
    {
        var sortVehicles = new SortItems(sortOrder);
        var filterVehicles = new FilterItems(searchString);
        var paginateVehicles = new PaginateItems<VehicleModel>(pageNumber, pageSize);

        return await _repository.GetVehicleModels(sortVehicles, filterVehicles, paginateVehicles);
    }
    
    public async Task<VehicleModel?> GetVehicleModelById(int id)
    {
        return await _repository.GetVehicleModelById(id);
    }

    public async Task<int> CreateVehicleModel(VehicleModel model) 
    {
        var existingModel = await this.GetVehicleModelById(model.Id);

        if (existingModel != null) 
        {
            return 404;
        }

        var existingVehicle = _vehicleService.GetVehicleById(model.MakeId);

        if (existingVehicle == null) 
        {
            return 400;
        }
        
        return await _repository.CreateVehicleModel(model);
    }

    public async Task<int> UpdateVehicleModel(int id, VehicleModel newModel)
    {
        if (id != newModel.Id) {
            return 400;
        }

        var oldModel = await this.GetVehicleModelById(id);

        if (oldModel == null) 
        {
            return 404;
        }

        var existingVehicle = await _vehicleService.GetVehicleById(newModel.MakeId);

        if (existingVehicle == null)
        {
            return 400;
        }
        
        return await _repository.UpdateVehicleModel(newModel, oldModel);
    }

    public async Task<int> DeleteVehicleModel(int id)
    {
        var vehicle = await this.GetVehicleModelById(id);

        if (vehicle == null) 
        {
            return 404;
        }
        
        await _repository.DeleteVehicleModel(vehicle);

        return 204;
    }
  
}