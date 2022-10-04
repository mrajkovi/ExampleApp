using ExampleApp.Models;
using ExampleApp.Data;
using ExampleApp.Helpers.Sort;
using ExampleApp.Helpers.Filter;
using ExampleApp.Helpers.Paginate;
using ExampleApp.Helpers.QueryObjects;
using ExampleApp.ViewModels.Vehicles;

namespace ExampleApp.Services;

public interface IVehicleMakeService 
{
    public Task GetVehicles(VehicleMakeQuery query, VehiclesPaginationViewModel vehiclesViewModel);
    public Task<VehicleMake?> GetVehicleById(int id);

    public Task<VehicleMake?> GetVehicleByName(string name);
    public Task<int> CreateVehicle(VehicleMake vehicle);
    public Task<int> UpdateVehicle(int id, VehicleMake vehicle);
    public Task<int> DeleteVehicle(int id);
}
public class VehicleMakeService : IVehicleMakeService
{

    private readonly IVehicleMakeRepository _repository = null!;

    public VehicleMakeService(IVehicleMakeRepository repository)
    {
        _repository = repository;
    }

    public async Task GetVehicles(VehicleMakeQuery query, VehiclesPaginationViewModel vehiclesPaginationViewModel) 
    {
        var sortVehicles = new SortItems(query.sortOrder);
        var filterVehicles = new FilterItems(query.searchString);
        var paginateVehicles = new PaginateItems<VehicleMake>(query.pageNumber, query.pageSize);

        var vehicles = await _repository.GetVehicles(sortVehicles, filterVehicles, paginateVehicles);
        
        vehiclesPaginationViewModel.Vehicles = vehicles;
        vehiclesPaginationViewModel.TotalSize = paginateVehicles.totalSize;
        vehiclesPaginationViewModel.PageNumber = paginateVehicles.pageNumber;
        vehiclesPaginationViewModel.PageSize = paginateVehicles.pageSize;
        vehiclesPaginationViewModel.FilterString = filterVehicles.filterString;
        vehiclesPaginationViewModel.SortOrder = sortVehicles.SortOrder;
    }
    
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return await _repository.GetVehicleById(id);
    }

    public async Task<VehicleMake?> GetVehicleByName(string name)
    {
        return await _repository.GetVehicleByName(name);
    }

    public async Task<int> CreateVehicle(VehicleMake vehicle) 
    {
        var existingVehicle = await this.GetVehicleByName(vehicle.Name);
        
        if (existingVehicle != null) 
        {
            return 404;
        }
        
        return await _repository.CreateVehicle(vehicle);
    }

    public async Task<int> UpdateVehicle(int id, VehicleMake newVehicle)
    {

        var oldvehicle = await this.GetVehicleById(id);

        if (oldvehicle == null) 
        {
            return 404;
        }

        var existingVehicle = await GetVehicleByName(newVehicle.Name);

        if (existingVehicle == null)
        {
            return await _repository.UpdateVehicle(newVehicle, oldvehicle);
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