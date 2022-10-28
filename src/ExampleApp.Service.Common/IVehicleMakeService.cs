using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;

namespace ExampleApp.Service.Common;

public interface IVehicleMakeService 
{
    public Task<int> CountVehicles(FilterItems<VehicleMakeEntity> filterVehicles);
    public Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems<VehicleMakeEntity> filterVehicles, PaginateItems<VehicleMakeEntity> paginateItems);
    public Task<VehicleMake?> GetVehicle(FilterItems<VehicleMakeEntity> filterVehicles);
    public Task<bool> CreateVehicle(VehicleMake newVehicle);
    public Task<bool> UpdateVehicle(FilterItems<VehicleMakeEntity> filterVehiclesById, VehicleMake newVehicle);
    public Task<bool> DeleteVehicle(FilterItems<VehicleMakeEntity> filterVehiclesById);
}
