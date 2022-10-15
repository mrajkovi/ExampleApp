using ExampleApp.Common;
using ExampleApp.Model;

namespace ExampleApp.Service.Common;

public interface IVehicleModelService 
{
    public Task<List<VehicleModel>> GetVehiclesModels(QueryDataSFP queryDataSFP);
    public Task<VehicleModel?> GetVehicleModelById(int id);
    public Task<VehicleModel?> GetVehicleModelByName(string name);
    public Task<bool> CreateVehicleModel(VehicleModel newVehicleModel);
    public Task<bool> UpdateVehicleModel(int id, VehicleModel newVehicleModel);
    public Task<bool> DeleteVehicleModel(int id);
}