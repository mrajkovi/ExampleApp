using Microsoft.AspNetCore.Mvc;
using ExampleApp.Services;
using ExampleApp.Models;
using ExampleApp.ViewModels.Vehicles;
using ExampleApp.Helpers.QueryObjects;
namespace ExampleApp.Controllers;

[Route("[controller]")]
public class VehicleMakeController : Controller
{
    private readonly IVehicleMakeService _service = null!;

    public VehicleMakeController(IVehicleMakeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> VehiclesView(VehicleMakeQuery query)
    {
        var vehiclesPaginationViewModel = new VehiclesPaginationViewModel();
        await _service.GetVehicles(query, vehiclesPaginationViewModel);
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
    public async Task<IActionResult> CreateVehicle(VehiclesPaginationViewModel vehicle)
    {
        VehicleMake newVehicle = new VehicleMake();
        newVehicle.Name = vehicle.Name;
        newVehicle.Abbrv = vehicle.Abbrv;
        var code = await _service.CreateVehicle(newVehicle);
        if (code == 201) 
        {
            return RedirectToAction(nameof(VehiclesView));
        } else 
        {
            return View("Error");
        }
    }
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, VehicleViewModel vehicle)
    {
        VehicleMake updatedVehicle = new VehicleMake();
        updatedVehicle.Abbrv = vehicle.Abbrv;
        updatedVehicle.Name = vehicle.Name;
        var code = await _service.UpdateVehicle(id, updatedVehicle);
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
            return RedirectToAction(nameof(VehiclesView));
        }
        var vehicleViewModel = new VehicleViewModel();
        vehicleViewModel.Name = vehicle.Name;
        vehicleViewModel.Abbrv = vehicle.Abbrv;
        return View("Update", vehicleViewModel);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var code = await _service.DeleteVehicle(id);
        if (code == 204) 
        {
            return RedirectToAction(nameof(VehiclesView));
        } else 
        {
            // return NotFound();
            return View("Error");
        }
    }
}
