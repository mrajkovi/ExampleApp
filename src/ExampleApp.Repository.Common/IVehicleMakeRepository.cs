using ExampleApp.Common;
using ExampleApp.Model;

namespace ExampleApp.Repository.Common;

public interface IVehicleMakeRepository 
{
    public Task<List<VehicleMake>> GetVehicles(QueryDataSFP queryDataSFP);
    public Task<VehicleMake?> GetVehicleById(int id);
    public Task<VehicleMake?> GetVehicleByName(string name);
    public Task CreateVehicle(VehicleMake vehicle);
    public Task UpdateVehicle(VehicleMake newVehicle, VehicleMake oldVehicle);
    public Task DeleteVehicle(VehicleMake vehicle);
}
