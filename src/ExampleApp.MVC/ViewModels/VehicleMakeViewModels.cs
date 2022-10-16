using ExampleApp.Model;
using System.ComponentModel.DataAnnotations;

namespace ExampleApp.MVC.ViewModels;

public class VehiclesPaginationViewModel
{
    public string Id { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Display(Name="Abbreviation")]
    public string Abbrv { get; set; } = null!;
    public List<VehicleMake> Vehicles { get; set; } = null!;
    public int TotalSize { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string SortOrder { get; set; } = null!;
    public string FilterString { get; set;} = null!;
}

public class VehicleViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Display(Name="Abbreviation")]
    public string Abbrv { get; set; } = null!;
}