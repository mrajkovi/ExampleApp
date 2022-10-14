using ExampleApp.Common;
using ExampleApp.Model.Common;

namespace ExampleApp.Service.Common;

public interface IVehicleModelService 

{
    public Task<List<IVehicleModel>> GetVehiclesModels(QueryModifier queryModifier);
    public Task<IVehicleModel?> GetVehicleModelById(int id);
    public Task<IVehicleModel?> GetVehicleModelByName(string name);
    public Task<bool> CreateVehicleModel(IVehicleModel newVehicleModel);
    public Task<bool> UpdateVehicleModel(int id, IVehicleModel newVehicleModel);
    public Task<bool> DeleteVehicleModel(int id);
}