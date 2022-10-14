using ExampleApp.Model.Common;
using System.ComponentModel.DataAnnotations;

namespace ExampleApp.ViewModels.Models;

public class VehiclesModelsPaginationViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Display(Name="Abbreviation")]
    public string Abbrv { get; set; } = null!;
    [Required]
    public int MakeId {get; set; }
    public List<IVehicleModel> Models { get; set; } = null!;
    public int TotalSize { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string SortOrder { get; set; } = null!;
    public string FilterString { get; set;} = null!;
}

public class VehicleModelsViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Display(Name="Abbreviation")]
    public string Abbrv { get; set; } = null!;
    [Required]
    public int MakeId { get; set; }
}