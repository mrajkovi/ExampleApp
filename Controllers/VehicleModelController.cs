using Microsoft.AspNetCore.Mvc;
using ExampleApp.Services;
using ExampleApp.Models;

namespace ExampleApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleModelController : Controller
{
    private readonly VehicleModelService _service = null!;

    public VehicleModelController(VehicleModelService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetModels(string? sortOrder, string? searchString, string? pageNumber, string? pageSize)
    {
        return Ok(await _service.GetAll(sortOrder, searchString, pageNumber, pageSize));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetModel(int id)
    {
        var vehicle = await _service.GetById(id);
        if (vehicle == null) 
        {
            return NotFound();
        }
        return Ok(vehicle);
    }
    [HttpPost]
    public async Task<IActionResult> NewModel(VehicleModel vehicleModel)
    {
        var code = await _service.Create(vehicleModel);
        // we will just return status 200
        if (code == 201) {
            return Ok();
        } else {
            return BadRequest();
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModel(int id, VehicleModel vehicleModel)
    {
        var code = await _service.Update(id, vehicleModel);
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
    public async Task<IActionResult> DeleteModel(int id)
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
