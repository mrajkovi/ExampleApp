using ExampleApp.Model.Common;
using ExampleApp.Common;

namespace ExampleApp.Service.Common;
public interface IVehicleMakeService 
{
    public Task<List<IVehicleMake>> GetVehicles(QueryModifier queryModifier);
    public Task<IVehicleMake?> GetVehicleById(int id);

    public Task<IVehicleMake?> GetVehicleByName(string name);
    public Task<bool> CreateVehicle(IVehicleMake newVehicle);
    public Task<bool> UpdateVehicle(int id, IVehicleMake newVehicle);
    public Task<bool> DeleteVehicle(int id);
}
