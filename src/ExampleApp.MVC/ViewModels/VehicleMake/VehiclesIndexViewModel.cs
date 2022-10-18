namespace ExampleApp.MVC.ViewModels;

public class VehiclesIndexViewModel
{
    public VehiclesViewModel VehiclesViewModel { get; set; } = null!;
    public DataManipulationViewModel DataManipulationViewModel { get; set; } = null!;
    public VehiclesIndexViewModel(VehiclesViewModel vehiclesViewModel, DataManipulationViewModel dataManipulationViewModel)
    {
        VehiclesViewModel = vehiclesViewModel;
        DataManipulationViewModel = dataManipulationViewModel;
    }
}