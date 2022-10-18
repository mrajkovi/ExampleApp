using ExampleApp.Model;

namespace ExampleApp.MVC.ViewModels;

public class VehiclesModelsIndexViewModel
{
    public VehiclesModelsViewModel VehiclesModelsViewModel { get; set; } = null!;
    public DataManipulationViewModel DataManipulationViewModel { get; set; } = null!;
    public VehiclesModelsIndexViewModel(VehiclesModelsViewModel vehiclesViewModel, DataManipulationViewModel dataManipulationViewModel)
    {
        VehiclesModelsViewModel = vehiclesViewModel;
        DataManipulationViewModel = dataManipulationViewModel;
    }
}