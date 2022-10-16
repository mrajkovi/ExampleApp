using Microsoft.AspNetCore.Mvc;
using ExampleApp.Service.Common;
using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.MVC.ViewModels;
using ExampleApp.MVC.Common;
using AutoMapper;

namespace ExampleApp.MVC.Controllers;

[Route("[controller]")]
public class VehicleModelController : Controller
{
    private readonly IVehicleModelService _service = null!;
    private readonly IMapper _mapper = null!;
    private readonly ILogger _logger = null!;
    public VehicleModelController(IVehicleModelService service, IMapper mapper, ILogger<VehicleModelController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> ModelsIndex(QueryDataSFP queryDataSFP)
    {
        _logger.LogInformation(EventID.GetItems, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            List<VehicleModel> models = await _service.GetVehiclesModels(queryDataSFP);
            VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel = new VehiclesModelsPaginationViewModel();
            vehiclesModelsPaginationViewModel = _mapper.Map<VehiclesModelsPaginationViewModel>(queryDataSFP);
            vehiclesModelsPaginationViewModel.Models = models;
            vehiclesModelsPaginationViewModel.TotalSize = await _service.CountVehiclesModels();
            return View("Index", vehiclesModelsPaginationViewModel);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleModel(int id)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            VehicleModel? vehicle = await _service.GetVehicleModelById(id);
            if (vehicle == null) 
            {
                _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                return NotFound();
            }
            return Ok(vehicle);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateVehicleModel(VehiclesModelsPaginationViewModel vehiclesModelsPaginationViewModel)
    {
        _logger.LogInformation(EventID.InsertItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            VehicleModel model = _mapper.Map<VehicleModel>(vehiclesModelsPaginationViewModel);
            bool succeeded = await _service.CreateVehicleModel(model);

            if (succeeded) 
            {
                return RedirectToAction(nameof(ModelsIndex));
            } 
            else 
            {
                _logger.LogWarning(EventID.BadRequest, "Bad request");
                return View("Error");
            }
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error");
        }
    }
    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateView(int id)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            VehicleModel? vehicle = await _service.GetVehicleModelById(id);
            if (vehicle == null)
            {
                _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                return RedirectToAction(nameof(ModelsIndex));
            }
            VehicleModelViewModel vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicle);
            return View("Update", vehicleModelViewModel);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error");
        }
    }
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelViewModel vehicleModelsViewModel)
    {
        _logger.LogInformation(EventID.UpdateItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
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
                _logger.LogWarning(EventID.BadRequest, "Bad request");
                return View("Error");
            }
        }
        catch(Exception exception)
        {
             _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error");
        }
    }
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        _logger.LogInformation(EventID.DeleteItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            bool succeeded = await _service.DeleteVehicleModel(id);
            if (succeeded) 
            {
                return RedirectToAction(nameof(ModelsIndex));
            } 
            else 
            {
                 _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                return View("Error");
            }
        } 
        catch(Exception exception)
        {
             _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error");
        }
    }
}
