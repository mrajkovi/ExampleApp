using Microsoft.EntityFrameworkCore;
using ExampleApp.Models;

namespace ExampleApp.Data;
public class ExampleAppContext : DbContext 
{
  public ExampleAppContext (DbContextOptions<ExampleAppContext> options) : base(options) {}
  public DbSet<VehicleMake> VehicleMake { get; set; } = null!;
  public DbSet<VehicleModel> VehicleModel { get; set; } = null!;
}