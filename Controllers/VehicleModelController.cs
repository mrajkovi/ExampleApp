using Microsoft.AspNetCore.Mvc;
using ExampleApp.Services;
using ExampleApp.Models;

namespace ExampleApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleModelController : Controller
{
    private readonly IVehicleModelService _service = null!;

    public VehicleModelController(IVehicleModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicleModels(string? sortOrder, string? searchString, string? pageNumber, string? pageSize)
    {
        return View("Index", await _service.GetVehicleModels(sortOrder, searchString, pageNumber, pageSize));
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
    public async Task<IActionResult> CreateVehicleModel(VehicleModel vehicleModel)
    {
        var code = await _service.CreateVehicleModel(vehicleModel);
        // we will just return status 200
        if (code == 201) {
            return Ok();
        } else {
            return BadRequest();
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModel vehicleModel)
    {
        var code = await _service.UpdateVehicleModel(id, vehicleModel);
        if (code == 204) 
        {
            return NoContent();
        } else if (code == 400) 
        {
            return BadRequest();
        } else 
        {
            return NotFound();
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        var code = await _service.DeleteVehicleModel(id);
        if (code == 204) 
        {
            return NoContent();
        } else 
        {
            return NotFound();
        }
    }
}
