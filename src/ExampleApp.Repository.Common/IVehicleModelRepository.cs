using ExampleApp.Model;
using ExampleApp.Common;

namespace ExampleApp.Repository.Common;

public interface IVehicleModelRepository 
{
    public Task<int> CountVehiclesModels();
    public Task<List<VehicleModel>> GetVehiclesModels(QueryDataSFP queryDataSFP);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<bool> CheckVehicleModelById(int id);
    public Task<bool> CheckVehicleModelByName(string name);
    public Task<VehicleModel?> GetVehicleModelByName(string name);
    public Task CreateVehicleModel(VehicleModel vehicle);
    public Task UpdateVehicleModel(VehicleModel updatedModel);
    public Task DeleteVehicleModel(VehicleModel model);
}