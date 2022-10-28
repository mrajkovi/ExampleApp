using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.DAL;

namespace ExampleApp.Service.Common;

public interface IVehicleModelService 
{
    public Task<int> CountVehiclesModels(FilterItems<VehicleModelEntity> filterModels);
    public Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems<VehicleModelEntity> filterModels, PaginateItems<VehicleModelEntity> paginateItems);
    public Task<VehicleModel?> GetVehicleModel(FilterItems<VehicleModelEntity> filterModels);
    public Task<bool> CreateVehicleModel(VehicleModel newVehicleModel);
    public Task<bool> UpdateVehicleModel(FilterItems<VehicleModelEntity> filterModelsById, VehicleModel newVehicleModel);
    public Task<bool> DeleteVehicleModel(FilterItems<VehicleModelEntity> fiterModelsById);
}