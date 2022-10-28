using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;

namespace ExampleApp.Repository.Common;

public interface IVehicleModelRepository 
{
    public Task<int> CountVehiclesModels(FilterItems<VehicleModelEntity> filterModels);
    public Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems<VehicleModelEntity> filterItems, PaginateItems<VehicleModelEntity> paginateItems);
    public Task<VehicleModel?> GetVehicleModel(FilterItems<VehicleModelEntity> filterItems);
    public Task<bool> CheckVehicleModel(FilterItems<VehicleModelEntity> filterItems);
    public Task CreateVehicleModel(VehicleModel vehicle);
    public Task UpdateVehicleModel(VehicleModel newVehicleModel);
    public Task DeleteVehicleModel(VehicleModel model);
}