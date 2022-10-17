using System.ComponentModel.DataAnnotations;

namespace ExampleApp.MVC.ViewModels;

public class VehicleViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [Display(Name="Abbreviation")]
    public string Abbrv { get; set; } = null!;
}