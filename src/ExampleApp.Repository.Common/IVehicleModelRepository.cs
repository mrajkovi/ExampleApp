using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;

namespace ExampleApp.Repository.Common;

public interface IVehicleModelRepository 
{
    public Task<int> CountVehiclesModels();
    public Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems<VehicleModelEntity> filterItems, PaginateItems<VehicleModelEntity> paginateItems);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<bool> CheckVehicleModelById(int id);
    public Task<bool> CheckVehicleModelByFilter(FilterItems<VehicleModelEntity> filterItems);
    public Task<VehicleModel?> GetVehicleModelByFilter(FilterItems<VehicleModelEntity> filterItems);
    public Task CreateVehicleModel(VehicleModel vehicle);
    public Task UpdateVehicleModel(VehicleModel updatedModel);
    public Task DeleteVehicleModel(VehicleModel model);
}