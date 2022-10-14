using Microsoft.EntityFrameworkCore;

namespace ExampleApp.DAL;

[Index(nameof(Name), IsUnique = true)]
public class VehicleMakeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Abbrv { get; set; } = null!;

    public List<VehicleModelEntity> Models { get; set; } = null!;
}