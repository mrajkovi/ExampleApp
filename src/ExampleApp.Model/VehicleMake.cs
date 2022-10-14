using ExampleApp.Model.Common;

namespace ExampleApp.Model;
public class VehicleMake : IVehicleMake
{
    public new int Id { get; set; }
    public new string Name { get; set; } = null!;
    public new string Abbrv { get; set; } = null!;
}
