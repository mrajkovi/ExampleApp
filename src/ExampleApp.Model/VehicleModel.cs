using ExampleApp.Model.Common;

namespace ExampleApp.Model;
public class VehicleModel : IVehicleModel
{
    public int Id { get; set; }
    public int MakeId { get; set; }
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;
}
