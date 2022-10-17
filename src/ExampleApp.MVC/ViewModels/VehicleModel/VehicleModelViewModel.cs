using System.ComponentModel.DataAnnotations;

namespace ExampleApp.MVC.ViewModels;

public class VehicleModelViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Display(Name="Abbreviation")]
    public string Abbrv { get; set; } = null!;
    [Required]
    public int MakeId { get; set; }
}