namespace ExampleApp.Models;
public class VehicleMake 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;

    public List<VehicleModel> Models { get; set; } = null!;
}

public class VehicleModel
{
    public int Id { get; set; }
    public int MakeId { get; set; }
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;

    public VehicleMake Make { get; set; } = null!;
}