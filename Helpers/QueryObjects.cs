namespace ExampleApp.Helpers.QueryObjects;

public class VehicleMakeQuery 
{
    public string? sortOrder { get; set; }
    public string? searchString { get; set; }
    public string? pageNumber { get; set; }
    public string? pageSize { get; set; }
}