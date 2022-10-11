using Microsoft.AspNetCore.Mvc;
using ExampleApp.Services;
using ExampleApp.Models;
using ExampleApp.Helpers.QueryObjects;
using ExampleApp.ViewModels.Models;
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
    public async Task<IActionResult> ModelsIndex(VehicleMakeQuery query)
    {
        var vehiclesPaginationViewModel = await _service.GetVehicleModels(query);
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
    public async Task<IActionResult> CreateVehicleModel(VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel)
    {
        var code = await _service.CreateVehicleModel(vehiclesModelsPaginationViewModel);

        if (code == 204) 
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
        var code = await _service.UpdateVehicleModel(id, vehicleModelsViewModel);
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
            return RedirectToAction(nameof(ModelsIndex));
        } else 
        {
            // return NotFound();
            return View("Error");
        }
    }
}
