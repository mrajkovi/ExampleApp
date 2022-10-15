using ExampleApp.Model;
using ExampleApp.Common;

namespace ExampleApp.Repository.Common;

public interface IVehicleModelRepository 
{
    public Task<List<VehicleModel>> GetVehiclesModels(QueryDataSFP queryDataSFP);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<VehicleModel?> GetVehicleModelByName(string name);
    public Task CreateVehicleModel(VehicleModel vehicle);
    public Task UpdateVehicleModel(VehicleModel newModel, VehicleModel oldModel);
    public Task DeleteVehicleModel(VehicleModel model);
}