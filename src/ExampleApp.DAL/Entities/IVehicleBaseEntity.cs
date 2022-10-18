namespace ExampleApp.DAL;

public interface IVehicleBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abbrv { get; set; }
}