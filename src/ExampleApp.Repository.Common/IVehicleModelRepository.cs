using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;

namespace ExampleApp.Repository.Common;

public interface IVehicleModelRepository 
{
    public Task<int> CountVehiclesModels();
    public Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems filterItems, PaginateItems<VehicleModelEntity> paginateItems);
    public Task<VehicleModel?> GetVehicleModel(FilterItems filterItems);
    public Task<bool> CheckVehicleModel(FilterItems filterItems);
    public Task CreateVehicleModel(VehicleModel vehicle);
    public Task UpdateVehicleModel(VehicleModel updatedModel);
    public Task DeleteVehicleModel(VehicleModel model);
}