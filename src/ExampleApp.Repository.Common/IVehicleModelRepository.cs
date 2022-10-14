using ExampleApp.Model.Common;
using ExampleApp.Common;

namespace ExampleApp.Repository.Common;

public interface IVehicleModelRepository 
{
    public Task<List<IVehicleModel>> GetVehiclesModels(QueryModifier queryModifier);
    public Task<IVehicleModel?> GetVehicleModelById(int id);
    public Task<IVehicleModel?> GetVehicleModelByName(string name);
    public Task<bool> CreateVehicleModel(IVehicleModel vehicle);
    public Task<bool> UpdateVehicleModel(IVehicleModel newModel, IVehicleModel oldModel);
    public Task<bool> DeleteVehicleModel(IVehicleModel model);
}