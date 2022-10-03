using Microsoft.EntityFrameworkCore;
namespace ExampleApp.Models;

[Index(nameof(Name), IsUnique = true)]
public class VehicleMake 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;

    public List<VehicleModel> Models { get; set; } = null!;
}

[Index(nameof(Name), IsUnique = true)]
public class VehicleModel
{
    public int Id { get; set; }
    public int MakeId { get; set; } // <navigation property name>Id is foreign key
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;

    public VehicleMake Make { get; set; } = null!;
}