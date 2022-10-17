using ExampleApp.Common;
using ExampleApp.DAL;
using ExampleApp.Model;

namespace ExampleApp.Repository.Common;

public interface IVehicleMakeRepository 
{
    public Task<int> CountVehicles();
    public Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems<VehicleMakeEntity> filterItems, PaginateItems<VehicleMakeEntity> paginateItems);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<VehicleMake?> GetVehicleByFilter(FilterItems<VehicleMakeEntity> filterItems);
    public Task<bool> CheckVehicleById(int id);
    public Task<bool> CheckVehicleByFilter(FilterItems<VehicleMakeEntity> filterItems);
    public Task CreateVehicle(VehicleMake vehicle);
    public Task UpdateVehicle(VehicleMake newVehicle);
    public Task DeleteVehicle(VehicleMake vehicle);
}
