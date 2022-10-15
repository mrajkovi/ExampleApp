using ExampleApp.Model;
using ExampleApp.Common;

namespace ExampleApp.Service.Common;

public interface IVehicleMakeService 
{
    public Task<List<VehicleMake>> GetVehicles(QueryDataSFP queryDataSFP);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<VehicleMake?> GetVehicleByName(string name);
    public Task<bool> CreateVehicle(VehicleMake newVehicle);
    public Task<bool> UpdateVehicle(int id, VehicleMake newVehicle);
    public Task<bool> DeleteVehicle(int id);
}
