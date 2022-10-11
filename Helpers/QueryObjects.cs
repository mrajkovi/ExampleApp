namespace ExampleApp.Helpers.QueryObjects;

public class VehicleMakeQuery 
{
    public string? SortOrder { get; set; }
    public string? SearchString { get; set; }
    public string? PageNumber { get; set; }
    public string? PageSize { get; set; }
}