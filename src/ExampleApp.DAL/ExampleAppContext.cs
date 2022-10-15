using Microsoft.EntityFrameworkCore;
namespace ExampleApp.DAL;
public class ExampleAppContext : DbContext 
{
    public ExampleAppContext (DbContextOptions<ExampleAppContext> options) : base(options) {}
    public DbSet<VehicleMakeEntity> VehicleMake { get; set; } = null!;
    public DbSet<VehicleModelEntity> VehicleModel { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleMakeEntity>()
            .HasMany(v => v.Models)
            .WithOne(m => m.Make)
            .OnDelete(DeleteBehavior.Cascade);
    }
}