using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;

namespace ExampleApp.Service.Common;

public interface IVehicleMakeService 
{
    public Task<int> CountVehicles();
    public Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems filterVehicles, PaginateItems<VehicleMakeEntity> paginateItems);
    public Task<VehicleMake?> GetVehicle(FilterItems filterVehicles);
    public Task<bool> CreateVehicle(VehicleMake newVehicle);
    public Task<bool> UpdateVehicle(FilterItems FilterVehiclesById, VehicleMake newVehicle);
    public Task<bool> DeleteVehicle(FilterItems FilterVehiclesById);
}
