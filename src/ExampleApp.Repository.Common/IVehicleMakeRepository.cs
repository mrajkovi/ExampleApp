using ExampleApp.Common;
using ExampleApp.DAL;
using ExampleApp.Model;

namespace ExampleApp.Repository.Common;

public interface IVehicleMakeRepository 
{
    public Task<int> CountVehicles();
    public Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems filterItems, PaginateItems<VehicleMakeEntity> paginateItems);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<VehicleMake?> GetVehicleByName(string name);
    public Task<bool> CheckVehicleById(int id);
    public Task<bool> CheckVehicleByName(string name);
    public Task CreateVehicle(VehicleMake vehicle);
    public Task UpdateVehicle(VehicleMake newVehicle);
    public Task DeleteVehicle(VehicleMake vehicle);
}
