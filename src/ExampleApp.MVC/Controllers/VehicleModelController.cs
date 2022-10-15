using Microsoft.AspNetCore.Mvc;
using ExampleApp.Service.Common;
using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.MVC.ViewModels.Models;
using AutoMapper;

namespace ExampleApp.MVC.Controllers;

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
    public async Task<IActionResult> ModelsIndex(QueryDataSFP queryDataSFP)
    {
        try
        {
            List<VehicleModel> models = await _service.GetVehiclesModels(queryDataSFP);
            VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel = new VehiclesModelsPaginationViewModel();
            vehiclesModelsPaginationViewModel = _mapper.Map<VehiclesModelsPaginationViewModel>(queryDataSFP);
            vehiclesModelsPaginationViewModel.Models = models;
            return View("Index", vehiclesModelsPaginationViewModel);
        }
        catch
        {
            return View("Error");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleModel(int id)
    {
        try
        {
            VehicleModel? vehicle = await _service.GetVehicleModelById(id);
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
    public async Task<IActionResult> CreateVehicleModel(VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel)
    {
        try
        {
            VehicleModel model = new VehicleModel();
            model = _mapper.Map<VehicleModel>(vehiclesModelsPaginationViewModel);
            bool succeeded = await _service.CreateVehicleModel(model);

            if (succeeded) 
            {
                return RedirectToAction(nameof(ModelsIndex));
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
            VehicleModel? vehicle = await _service.GetVehicleModelById(id);
            if (vehicle == null)
            {
                return RedirectToAction(nameof(ModelsIndex));
            }
            var vehicleModelsViewModel = _mapper.Map<VehicleModelsViewModel>(vehicle);
            return View("Update", vehicleModelsViewModel);
        }
        catch
        {
            return View("Error");
        }
    }
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelsViewModel vehicleModelsViewModel)
    {
        try
        {
            VehicleModel model = new VehicleModel();
            model = _mapper.Map<VehicleModel>(vehicleModelsViewModel);
            bool succeeded = await _service.UpdateVehicleModel(id, model);
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
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        try
        {
            bool succeeded = await _service.DeleteVehicleModel(id);
            if (succeeded) 
            {
                return RedirectToAction(nameof(ModelsIndex));
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
}
