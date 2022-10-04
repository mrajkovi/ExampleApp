using Microsoft.AspNetCore.Mvc;
using ExampleApp.Services;
using ExampleApp.Models;
using ExampleApp.Helpers.QueryObjects;
using ExampleApp.ViewModels.Models;
namespace ExampleApp.Controllers;

[Route("[controller]")]
public class VehicleModelController : Controller
{
    private readonly IVehicleModelService _service = null!;

    public VehicleModelController(IVehicleModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ModelsView(VehicleMakeQuery query)
    {
        var vehiclesPaginationViewModel = new VehiclesModelsPaginationViewModel();
        await _service.GetVehicleModels(query, vehiclesPaginationViewModel);
        return View("Index", vehiclesPaginationViewModel);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleModel(int id)
    {
        var vehicle = await _service.GetVehicleModelById(id);
        if (vehicle == null) 
        {
            return NotFound();
        }
        return Ok(vehicle);
    }
    [HttpPost]
    public async Task<IActionResult> CreateVehicleModel(VehicleModelsViewModel vehicleModelsViewModel)
    {
        var vehicleModel = new VehicleModel();
        vehicleModel.Name = vehicleModelsViewModel.Name;
        vehicleModel.Abbrv = vehicleModelsViewModel.Abbrv;
        vehicleModel.MakeId = vehicleModelsViewModel.MakeId;
        var code = await _service.CreateVehicleModel(vehicleModel);

        if (code == 204) 
        {
            return RedirectToAction(nameof(ModelsView));
        } else 
        {
            return View("Error");
        }
    }

    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateView(int id)
    {
        var vehicle = await _service.GetVehicleModelById(id);
        if (vehicle == null)
        {
            return RedirectToAction(nameof(ModelsView));
        }
        var vehicleViewModel = new VehicleModelsViewModel();
        vehicleViewModel.Name = vehicle.Name;
        vehicleViewModel.Abbrv = vehicle.Abbrv;
        vehicleViewModel.MakeId = vehicle.MakeId;
        return View("Update", vehicleViewModel);
    }
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelsViewModel modelModel)
    {
        VehicleModel model = new VehicleModel();
        model.Name = modelModel.Name;
        model.Abbrv = modelModel.Abbrv;
        model.MakeId = modelModel.MakeId;
        var code = await _service.UpdateVehicleModel(id, model);
        if (code == 204) 
        {
            return RedirectToAction(nameof(UpdateView), new { id = id });
        } else if (code == 400) 
        {
            //return BadRequest();
            return View("Error");
        } else 
        {
            //return NotFound();
            return View("Error");
        }
        
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        var code = await _service.DeleteVehicleModel(id);
        if (code == 204) 
        {
            return RedirectToAction(nameof(ModelsView));
        } else 
        {
            // return NotFound();
            return View("Error");
        }
    }
}
