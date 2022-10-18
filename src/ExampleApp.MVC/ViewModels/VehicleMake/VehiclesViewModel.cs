namespace ExampleApp.MVC.ViewModels;

public class VehiclesViewModel
{
    public VehicleViewModel VehicleViewModel { get; set; } = null!;
    public List<VehicleViewModel> Vehicles { get; set; } = null!;
}