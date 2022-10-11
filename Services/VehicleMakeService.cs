using ExampleApp.Models;
using ExampleApp.Repository;
using ExampleApp.Helpers.Sort;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.Helpers.QueryObjects;
using ExampleApp.ViewModels.Vehicles;
using AutoMapper;

namespace ExampleApp.Services;

public interface IVehicleMakeService 
{
    public Task<VehiclesPaginationViewModel> GetVehicles(VehicleMakeQuery query);
    public Task<VehicleMake?> GetVehicleById(int id);

    public Task<VehicleMake?> GetVehicleByName(string name);
    public Task<int> CreateVehicle(VehiclesPaginationViewModel vehiclesPaginationViewModel);
    public Task<int> UpdateVehicle(int id, VehicleViewModel vehicleViewModel);
    public Task<int> DeleteVehicle(int id);
}
public class VehicleMakeService : IVehicleMakeService
{

    private readonly IVehicleMakeRepository _repository = null!;
    private readonly IMapper _mapper;

    public VehicleMakeService(IVehicleMakeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<VehiclesPaginationViewModel> GetVehicles(VehicleMakeQuery query) 
    {
        var sortVehicles = new SortItems(query.SortOrder);
        var filterVehicles = new FilterItems(query.SearchString);
        var paginateVehicles = new PaginateItems<VehicleMake>(query.PageNumber, query.PageSize);

        var vehicles = await _repository.GetVehicles(sortVehicles, filterVehicles, paginateVehicles);

        var vehiclesPaginationViewModel = _mapper.Map<VehiclesPaginationViewModel>(paginateVehicles);
        vehiclesPaginationViewModel = _mapper.Map(filterVehicles, vehiclesPaginationViewModel);
        vehiclesPaginationViewModel = _mapper.Map(sortVehicles, vehiclesPaginationViewModel);
        
        vehiclesPaginationViewModel.Vehicles = vehicles;
        
        return vehiclesPaginationViewModel;
    }
    
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return await _repository.GetVehicleById(id);
    }

    public async Task<VehicleMake?> GetVehicleByName(string name)
    {
        return await _repository.GetVehicleByName(name);
    }

    public async Task<int> CreateVehicle(VehiclesPaginationViewModel vehiclesPaginationViewModel) 
    {

        var vehicle = _mapper.Map<VehicleMake>(vehiclesPaginationViewModel);
       
        var existingVehicle = await this.GetVehicleByName(vehicle.Name);
        
        if (existingVehicle != null) 
        {
            return 404;
        }
        
        return await _repository.CreateVehicle(vehicle);
    }

    public async Task<int> UpdateVehicle(int id, VehicleViewModel vehicleViewModel)
    {
        var newVehicle = _mapper.Map<VehicleMake>(vehicleViewModel);

        var oldVehicle = await this.GetVehicleById(id);

        if (oldVehicle == null) 
        {
            return 404;
        }

        var existingVehicle = await GetVehicleByName(newVehicle.Name);

        if (existingVehicle == null)
        {
            return await _repository.UpdateVehicle(newVehicle, oldVehicle);
        } else
        {
            return 400;
        }
        
    }

    public async Task<int> DeleteVehicle(int id)
    {
        var vehicle = await this.GetVehicleById(id);
        
        if (vehicle == null) 
        {
            return 404;
        }
        
        await _repository.DeleteVehicle(vehicle);

        return 204;
    }
  
}