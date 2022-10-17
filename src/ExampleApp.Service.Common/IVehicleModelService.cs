using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.DAL;

namespace ExampleApp.Service.Common;

public interface IVehicleModelService 
{
    public Task<int> CountVehiclesModels();
    public Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems<VehicleModelEntity> filterItems, PaginateItems<VehicleModelEntity> paginateItems);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<VehicleModel?> GetVehicleModelByFilter(FilterItems<VehicleModelEntity> filterItems);
    public Task<bool> CreateVehicleModel(VehicleModel newVehicleModel);
    public Task<bool> UpdateVehicleModel(int id, VehicleModel newVehicleModel);
    public Task<bool> DeleteVehicleModel(int id);
}