using Microsoft.AspNetCore.Mvc;
using ExampleApp.Service.Common;
using ExampleApp.MVC.ViewModels.Vehicles;
using ExampleApp.Common;
using ExampleApp.Model;
using AutoMapper;

namespace ExampleApp.MVC.Controllers;

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
    public async Task<IActionResult> VehiclesIndex(QueryDataSFP queryDataSFP)
    {
        try 
        {
            List<VehicleMake> vehicles = await _service.GetVehicles(queryDataSFP);
            VehiclesPaginationViewModel vehiclesPaginationViewModel = new VehiclesPaginationViewModel();

            vehiclesPaginationViewModel = _mapper.Map<VehiclesPaginationViewModel>(queryDataSFP);
            vehiclesPaginationViewModel.Vehicles = vehicles;

            return View("Index", vehiclesPaginationViewModel);
        }
        catch
        {
            return View("Error"); 
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        try
        {
            VehicleMake? vehicle = await _service.GetVehicleById(id);
            if (vehicle == null) 
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetVehicle(string name)
    {
        try 
        {
            VehicleMake? vehicle = await _service.GetVehicleByName(name);
            if (vehicle == null) 
            {
                return NotFound();
            }
            return Ok(vehicle);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateVehicle(VehiclesPaginationViewModel vehiclesPaginationViewModel)
    {
        try
        {
            VehicleMake newVehicle = new VehicleMake();
            newVehicle = _mapper.Map<VehicleMake>(vehiclesPaginationViewModel);
            bool succeeded = await _service.CreateVehicle(newVehicle);
            if (succeeded) 
            {
                return RedirectToAction(nameof(VehiclesIndex));
            } else 
            {
                return View("Error");
            }
        }
        catch
        {
            return View("Error");
        }
    }
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, VehicleViewModel vehicleViewModel)
    {
        try 
        {
            VehicleMake newVehicle = new VehicleMake();
            newVehicle = _mapper.Map<VehicleMake>(vehicleViewModel);
            bool succeeded = await _service.UpdateVehicle(id, newVehicle);
            if (succeeded) 
            {
                return RedirectToAction(nameof(UpdateView), new { id = id });
            } 
            else 
            {
            
                return View("Error");
            }
        }
        catch
        {
            return View("Error");
        }       
    }
    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateView(int id)
    {
        try
        {
            VehicleMake? existingVehicle = await _service.GetVehicleById(id);
            if (existingVehicle == null)
            {
                return RedirectToAction(nameof(VehiclesIndex));
            }
            VehicleViewModel vehicleViewModel = _mapper.Map<VehicleViewModel>(existingVehicle);
            return View("Update", vehicleViewModel);
        }
        catch
        {
            return View("Error");
        }
    }
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        try
        {
            bool succeeded = await _service.DeleteVehicle(id);
            if (succeeded) 
            {
                return RedirectToAction(nameof(VehiclesIndex));
            } 
            else 
            {
                return View("Error");
            }
        }
        catch
        {
            return View("Error");
        }
    }
}
