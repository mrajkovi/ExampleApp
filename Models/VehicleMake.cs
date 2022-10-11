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