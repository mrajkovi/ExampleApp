using Microsoft.AspNetCore.Mvc;
using ExampleApp.Service.Common;
using ExampleApp.Common;
using ExampleApp.Model.Common;
using ExampleApp.WebAPI.ViewModels.Models;
using AutoMapper;
namespace ExampleApp.Controllers;

[Route("[controller]")]
public class VehicleModelController : Controller
{
    private readonly IVehicleModelService _service = null!;
    private readonly IMapper _mapper = null!;

    public VehicleModelController(IVehicleModelService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> ModelsIndex(QueryModifier queryModifier)
    {
        var models = await _service.GetVehiclesModels(queryModifier);
        var vehiclesModelsPaginationViewModel = new VehiclesModelsPaginationViewModel();
        vehiclesModelsPaginationViewModel = _mapper.Map<VehiclesModelsPaginationViewModel>(queryModifier);
        vehiclesModelsPaginationViewModel.Models = models;
        return View("Index", vehiclesModelsPaginationViewModel);
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
    public async Task<IActionResult> CreateVehicleModel(VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel)
    {
        var model = new IVehicleModel();
        model = _mapper.Map<IVehicleModel>(vehiclesModelsPaginationViewModel);
        var succeeded = await _service.CreateVehicleModel(model);

        if (succeeded) 
        {
            return RedirectToAction(nameof(ModelsIndex));
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
            return RedirectToAction(nameof(ModelsIndex));
        }
        var vehicleModelsViewModel = _mapper.Map<VehicleModelsViewModel>(vehicle);
        return View("Update", vehicleModelsViewModel);
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelsViewModel vehicleModelsViewModel)
    {
        var model = new IVehicleModel();
        model = _mapper.Map<IVehicleModel>(vehicleModelsViewModel);
        var succeeded = await _service.UpdateVehicleModel(id, model);
        if (succeeded) 
        {
            return RedirectToAction(nameof(UpdateView), new { id = id });
        } else 
        {
            //return NotFound();
            return View("Error");
        }
        
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        var succeeded = await _service.DeleteVehicleModel(id);
        if (succeeded) 
        {
            return RedirectToAction(nameof(ModelsIndex));
        } else 
        {
            // return NotFound();
            return View("Error");
        }
    }
}
