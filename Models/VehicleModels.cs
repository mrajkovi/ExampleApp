namespace ExampleApp.Models;
public class VehicleMake 
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Abbrv { get; set; }
}

public class VehicleModel
{
  public int Id { get; set; }
  public int MakeId { get; set; }
  public string Name { get; set; }
  public string Abbrv { get; set; }
}