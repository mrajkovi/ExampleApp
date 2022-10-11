using ExampleApp.Models;
using ExampleApp.Repository;
using ExampleApp.Helpers.Sort;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.ViewModels.Models;
using ExampleApp.Helpers.QueryObjects;
using AutoMapper;
namespace ExampleApp.Services;

public interface IVehicleModelService 
{
    public Task<VehiclesModelsPaginationViewModel> GetVehicleModels(VehicleMakeQuery query);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<VehicleModel?> GetVehicleModelByName(string name);
    public Task<int> CreateVehicleModel(VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel);
    public Task<int> UpdateVehicleModel(int id, VehicleModelsViewModel vehicleModelsViewModel);
    public Task<int> DeleteVehicleModel(int id);
}
public class VehicleModelService : IVehicleModelService
{
    private readonly IVehicleModelRepository _repository = null!;
    private readonly IVehicleMakeService _vehicleService = null!;
    private readonly IMapper _mapper = null!;


    public VehicleModelService(IVehicleModelRepository repository, IVehicleMakeService service, IMapper mapper)
    {
        _repository = repository;
        _vehicleService = service;
        _mapper = mapper;
    }

    public async Task<VehiclesModelsPaginationViewModel> GetVehicleModels(VehicleMakeQuery query) 
    {
        var sortModels = new SortItems(query.SortOrder);
        var filterModels = new FilterItems(query.SearchString);
        var paginateModels = new PaginateItems<VehicleModel>(query.PageNumber, query.PageSize);

        var models = await _repository.GetVehicleModels(sortModels, filterModels, paginateModels);

        var vehiclesModelsPaginationViewModel = _mapper.Map<VehiclesModelsPaginationViewModel>(paginateModels);
        vehiclesModelsPaginationViewModel = _mapper.Map<VehiclesModelsPaginationViewModel>(filterModels);
        vehiclesModelsPaginationViewModel = _mapper.Map<VehiclesModelsPaginationViewModel>(sortModels);
        vehiclesModelsPaginationViewModel.Models = models;

        return vehiclesModelsPaginationViewModel;
    }
    
    public async Task<VehicleModel?> GetVehicleModelById(int id)
    {
        return await _repository.GetVehicleModelById(id);
    }

    public async Task<VehicleModel?> GetVehicleModelByName(string name)
    {
        return await _repository.GetVehicleModelByName(name);
    }

    public async Task<int> CreateVehicleModel(VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel) 
    {
        var model = _mapper.Map<VehicleModel>(vehiclesModelsPaginationViewModel);

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

    public async Task<int> UpdateVehicleModel(int id, VehicleModelsViewModel vehicleModelsViewModel)
    {

        var newModel = _mapper.Map<VehicleModel>(vehicleModelsViewModel);

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