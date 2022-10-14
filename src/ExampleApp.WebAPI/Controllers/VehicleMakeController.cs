using Microsoft.AspNetCore.Mvc;
using ExampleApp.Service.Common;
using ExampleApp.WebAPI.ViewModels.Vehicles;
using ExampleApp.Common;
using ExampleApp.Model.Common;
using AutoMapper;
namespace ExampleApp.WebAPI.Controllers;

[Route("[controller]")]
public class VehicleMakeController : Controller
{
    private readonly IVehicleMakeService _service = null!;
    private readonly IMapper _mapper = null!;

    public VehicleMakeController(IVehicleMakeService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> VehiclesIndex(QueryModifier queryModifier)
    {
        var vehicles = await _service.GetVehicles(queryModifier);
        var vehiclesPaginationViewModel = new VehiclesPaginationViewModel();
        vehiclesPaginationViewModel = _mapper.Map<VehiclesPaginationViewModel>(queryModifier);
        vehiclesPaginationViewModel.Vehicles = vehicles;

        return View("Index", vehiclesPaginationViewModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        var vehicle = await _service.GetVehicleById(id);
        if (vehicle == null) 
        {
            return NotFound();
        }
        return Ok(vehicle);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetVehicle(string name)
    {
        var vehicle = await _service.GetVehicleByName(name);
        if (vehicle == null) 
        {
            return NotFound();
        }
        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle(VehiclesPaginationViewModel vehiclesPaginationViewModel)
    {
        var newVehicle = new IVehicleMake();
        newVehicle = _mapper.Map<IVehicleMake>(vehiclesPaginationViewModel);
        var succeeded = await _service.CreateVehicle(newVehicle);
        if (succeeded) 
        {
            return RedirectToAction(nameof(VehiclesIndex));
        } else 
        {
            return View("Error");
        }
    }

    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, VehicleViewModel vehicleViewModel)
    {
        var newVehicle = new IVehicleMake();
        newVehicle = _mapper.Map<IVehicleMake>(vehicleViewModel);
        var succeeded = await _service.UpdateVehicle(id, newVehicle);
        if (succeeded) 
        {
            return RedirectToAction(nameof(UpdateView), new { id = id });
        } else 
        {
            // return NotFound()
            return View("Error");
        }    
    }

    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateView(int id)
    {
        var existingVehicle = await _service.GetVehicleById(id);
        if (existingVehicle == null)
        {
            return RedirectToAction(nameof(VehiclesIndex));
        }
        var vehicleViewModel = _mapper.Map<VehicleViewModel>(existingVehicle);
        return View("Update", vehicleViewModel);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var code = await _service.DeleteVehicle(id);
        if (!code) 
        {
            return RedirectToAction(nameof(VehiclesIndex));
        } else 
        {
            // return NotFound();
            return View("Error");
        }
    }
}
