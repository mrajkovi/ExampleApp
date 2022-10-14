using ExampleApp.Common;
using ExampleApp.Model.Common;

namespace ExampleApp.Repository.Common;
public interface IVehicleMakeRepository 
{
    public Task<List<IVehicleMake>> GetVehicles(QueryModifier queryModifier);
    public Task<IVehicleMake?> GetVehicleById(int id);
    public Task<IVehicleMake?> GetVehicleByName(string name);
    public Task<bool> CreateVehicle(IVehicleMake vehicle);
    public Task<bool> UpdateVehicle(IVehicleMake newVehicle, IVehicleMake oldVehicle);
    public Task<bool> DeleteVehicle(IVehicleMake vehicle);
}
