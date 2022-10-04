using ExampleApp.Models;
using ExampleApp.Data;
using ExampleApp.Helpers.Sort;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.ViewModels.Models;
using ExampleApp.Helpers.QueryObjects;
namespace ExampleApp.Services;

public interface IVehicleModelService 
{
    public Task GetVehicleModels(VehicleMakeQuery query, VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<VehicleModel?> GetVehicleModelByName(string name);
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

    public async Task GetVehicleModels(VehicleMakeQuery query, VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel) 
    {
        var sortModels = new SortItems(query.sortOrder);
        var filterModels = new FilterItems(query.searchString);
        var paginateModels = new PaginateItems<VehicleModel>(query.pageNumber, query.pageSize);

        var models = await _repository.GetVehicleModels(sortModels, filterModels, paginateModels);

        vehiclesModelsPaginationViewModel.Models = models;
        vehiclesModelsPaginationViewModel.TotalSize = paginateModels.totalSize;
        vehiclesModelsPaginationViewModel.PageNumber = paginateModels.pageNumber;
        vehiclesModelsPaginationViewModel.PageSize = paginateModels.pageSize;
        vehiclesModelsPaginationViewModel.FilterString = filterModels.filterString;
        vehiclesModelsPaginationViewModel.SortOrder = sortModels.SortOrder;
    }
    
    public async Task<VehicleModel?> GetVehicleModelById(int id)
    {
        return await _repository.GetVehicleModelById(id);
    }

    public async Task<VehicleModel?> GetVehicleModelByName(string name)
    {
        return await _repository.GetVehicleModelByName(name);
    }

    public async Task<int> CreateVehicleModel(VehicleModel model) 
    {
        var existingModel = await this.GetVehicleModelByName(model.Name);

        if (existingModel != null) 
        {
            return 404;
        }

        var existingVehicle = await _vehicleService.GetVehicleById(model.MakeId);

        if (existingVehicle == null) 
        {
            return 400;
        }
        
        return await _repository.CreateVehicleModel(model);
    }

    public async Task<int> UpdateVehicleModel(int id, VehicleModel newModel)
    {

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

        var existingModel = await this.GetVehicleModelByName(newModel.Name);

        if (existingModel != null && id != existingModel.Id)
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