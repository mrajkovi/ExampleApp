using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.DAL;

namespace ExampleApp.Service.Common;

public interface IVehicleModelService 
{
    public Task<int> CountVehiclesModels();
    public Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems filterModels, PaginateItems<VehicleModelEntity> paginateItems);
    public Task<VehicleModel?> GetVehicleModel(FilterItems filterModels);
    public Task<bool> CreateVehicleModel(VehicleModel newVehicleModel);
    public Task<bool> UpdateVehicleModel(FilterItems filterModelsById, VehicleModel newVehicleModel);
    public Task<bool> DeleteVehicleModel(FilterItems FiterModelsById);
}