using Microsoft.EntityFrameworkCore;

namespace ExampleApp.DAL;

[Index(nameof(Name), IsUnique = true)]
public class VehicleModelEntity
{
    public int Id { get; set; }
    public int MakeId { get; set; } // <navigation property name>Id is foreign key
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;
    public VehicleMakeEntity Make { get; set; } = null!;
}