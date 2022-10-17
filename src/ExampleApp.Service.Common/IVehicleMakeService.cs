using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;

namespace ExampleApp.Service.Common;

public interface IVehicleMakeService 
{
    public Task<int> CountVehicles();
    public Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems filterItems, PaginateItems<VehicleMakeEntity> paginateItems);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<VehicleMake?> GetVehicleByName(string name);
    public Task<bool> CreateVehicle(VehicleMake newVehicle);
    public Task<bool> UpdateVehicle(int id, VehicleMake newVehicle);
    public Task<bool> DeleteVehicle(int id);
}
