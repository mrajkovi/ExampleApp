using ExampleApp.Common;
using ExampleApp.DAL;
using ExampleApp.Model;

namespace ExampleApp.Repository.Common;

public interface IVehicleMakeRepository 
{
    public Task<int> CountVehicles(FilterItems<VehicleMakeEntity> filterVehicles);
    public Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems<VehicleMakeEntity> filterItems, PaginateItems<VehicleMakeEntity> paginateItems);
    public Task<VehicleMake?> GetVehicle(FilterItems<VehicleMakeEntity> filterItems);
    public Task<bool> CheckVehicle(FilterItems<VehicleMakeEntity> filterItems);
    public Task CreateVehicle(VehicleMake vehicle);
    public Task UpdateVehicle(VehicleMake newVehicle);
    public Task DeleteVehicle(VehicleMake vehicle);
}
