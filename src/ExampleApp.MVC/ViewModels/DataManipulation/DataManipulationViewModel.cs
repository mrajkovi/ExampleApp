namespace ExampleApp.MVC.ViewModels;

public class DataManipulationViewModel
{
    public int TotalSize { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string SortOrder { get; set; } = null!;
    public string SearchString { get; set;} = null!;
    public bool SearchByNumber { get; set; }
}