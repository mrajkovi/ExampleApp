using Microsoft.AspNetCore.Mvc;
using ExampleApp.Services;
using ExampleApp.Models;

namespace ExampleApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleMakeController : Controller
{
    private readonly VehicleMakeService _service = null!;

    public VehicleMakeController(VehicleMakeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicles(string? sortOrder, string? searchString, string? pageNumber, string? pageSize)
    {
        return Ok(await _service.GetAll(sortOrder, searchString, pageNumber, pageSize));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        var vehicle = await _service.GetById(id);
        if (vehicle == null) 
        {
            return NotFound();
        }
        return Ok(vehicle);
    }
    [HttpPost]
    public async Task<IActionResult> NewVehicle(VehicleMake vehicle)
    {
        var code = await _service.Create(vehicle);
        if (code == 201) 
        {
            // we will just respond with status 200
            return Ok();
        } else 
        {
            return BadRequest();
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, VehicleMake vehicle)
    {
        var code = await _service.Update(id, vehicle);
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
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        var code = await _service.Delete(id);
        if (code == 204) 
        {
            return NoContent();
        } else 
        {
            return NotFound();
        }
    }
}
