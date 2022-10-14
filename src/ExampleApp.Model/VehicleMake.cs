using ExampleApp.Model.Common;

namespace ExampleApp.Model;
public class VehicleMake : IVehicleMake
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;
}
