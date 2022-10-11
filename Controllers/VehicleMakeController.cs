using Microsoft.AspNetCore.Mvc;
using ExampleApp.Services;
using ExampleApp.ViewModels.Vehicles;
using ExampleApp.Helpers.QueryObjects;
using AutoMapper;
namespace ExampleApp.Controllers;

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
    public async Task<IActionResult> VehiclesIndex(VehicleMakeQuery query)
    {
        var vehiclesPaginationViewModel = await _service.GetVehicles(query);
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
        var code = await _service.CreateVehicle(vehiclesPaginationViewModel);
        if (code == 201) 
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
        var code = await _service.UpdateVehicle(id, vehicleViewModel);
        if (code == 204) 
        {
            return RedirectToAction(nameof(UpdateView), new { id = id });
        } else if (code == 400) 
        {
            // return BadRequest()
            return View("Error");
        } else 
        {
            // return NotFound()
            return View("Error");
        }    
    }

    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateView(int id)
    {
        var vehicle = await _service.GetVehicleById(id);
        if (vehicle == null)
        {
            return RedirectToAction(nameof(VehiclesIndex));
        }
        var vehicleViewModel = _mapper.Map<VehicleViewModel>(vehicle);
        return View("Update", vehicleViewModel);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var code = await _service.DeleteVehicle(id);
        if (code == 204) 
        {
            return RedirectToAction(nameof(VehiclesIndex));
        } else 
        {
            // return NotFound();
            return View("Error");
        }
    }
}
