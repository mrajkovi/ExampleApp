using Microsoft.AspNetCore.Mvc;
using ExampleApp.Service.Common;
using ExampleApp.MVC.ViewModels;
using ExampleApp.MVC.Common;
using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.DAL;
using AutoMapper;

namespace ExampleApp.MVC.Controllers;

[Route("[controller]")]
public class VehicleMakeController : Controller
{
    private readonly IVehicleMakeService _service = null!;
    private readonly IMapper _mapper = null!;
    private readonly ILogger _logger = null!;
    public VehicleMakeController(IVehicleMakeService service, IMapper mapper, ILogger<VehicleMakeController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> VehiclesIndex(QueryDataManipulation queryDataManipulation)
    {
        _logger.LogInformation(EventID.GetItems, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        
        try 
        {
            var sortItems = new SortItems<VehicleMakeEntity>(queryDataManipulation.SortOrder);
            FilterItems<VehicleMakeEntity> filterVehicles;
            
            if (queryDataManipulation.SearchByNumber)
            {
                filterVehicles = new FilterItems<VehicleMakeEntity>(queryDataManipulation.SearchString, "id");
            }
            else
            {
                filterVehicles = new FilterItems<VehicleMakeEntity>(queryDataManipulation.SearchString, "name_abbrv");
            }

            var paginateItems = new PaginateItems<VehicleMakeEntity>(queryDataManipulation.PageNumber, queryDataManipulation.PageSize);

            List<VehicleMake> vehicles = await _service.GetVehicles(sortItems, filterVehicles, paginateItems);

            VehiclesViewModel vehiclesViewModel = new VehiclesViewModel();
            vehiclesViewModel.Vehicles = _mapper.Map<List<VehicleViewModel>>(vehicles);
            DataManipulationViewModel dataManipulationViewModel = _mapper.Map<DataManipulationViewModel>(queryDataManipulation);
            dataManipulationViewModel.TotalSize = await _service.CountVehicles(filterVehicles);
            VehiclesIndexViewModel vehiclesIndexViewModel = new VehiclesIndexViewModel(vehiclesViewModel, dataManipulationViewModel);

            return View("Index", vehiclesIndexViewModel);
        }
        catch (Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error"); 
        }
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        
        try
        {
            var filterVehiclesbyId = new FilterItems<VehicleMakeEntity>(id.ToString(), "id");
            VehicleMake? vehicleMake = await _service.GetVehicle(filterVehiclesbyId);
            
            if (vehicleMake == null) 
            {
                _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                
                return NotFound();
            }

            return Ok(vehicleMake);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpGet("name/{name:alpha}")]
    public async Task<IActionResult> GetVehicle(string name)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        
        try 
        {
            FilterItems<VehicleMakeEntity> filterVehiclesByNameOrAbbrv = new FilterItems<VehicleMakeEntity>(name, "name_abbrv");
            VehicleMake? vehicleMake = await _service.GetVehicle(filterVehiclesByNameOrAbbrv);
            
            if (vehicleMake == null) 
            {
                _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                return NotFound();
            }
            
            return Ok(vehicleMake);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateVehicle(VehicleViewModel vehicleViewModel)
    {
        _logger.LogInformation(EventID.InsertItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            VehicleMake newVehicle = _mapper.Map<VehicleMake>(vehicleViewModel);
            bool succeeded = await _service.CreateVehicle(newVehicle);
            if (succeeded) 
            {
                return RedirectToAction(nameof(VehiclesIndex));
            } else 
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
    [HttpPost("update/{id:int}")]
    public async Task<IActionResult> UpdateVehicle(int id, VehicleViewModel vehicleViewModel)
    {
        _logger.LogInformation(EventID.UpdateItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try 
        {
            var filterVehiclesById = new FilterItems<VehicleMakeEntity>(id.ToString(), "id");
            VehicleMake newVehicle = _mapper.Map<VehicleMake>(vehicleViewModel);
            bool succeeded = await _service.UpdateVehicle(filterVehiclesById, newVehicle);
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
    [HttpGet("update/{id:int}")]
    public async Task<IActionResult> UpdateView(int id)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            var filterVehiclesById = new FilterItems<VehicleMakeEntity>(id.ToString(), "id");
            VehicleMake? existingVehicle = await _service.GetVehicle(filterVehiclesById);
            if (existingVehicle == null)
            {
                _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                return RedirectToAction(nameof(VehiclesIndex));
            }
            VehicleViewModel vehicleViewModel = _mapper.Map<VehicleViewModel>(existingVehicle);
            return View("Update", vehicleViewModel);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error");
        }
    }
    [HttpPost("delete/{id:int}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        _logger.LogInformation(EventID.DeleteItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            var filterVehiclesById = new FilterItems<VehicleMakeEntity>(id.ToString(), "id");
            bool succeeded = await _service.DeleteVehicle(filterVehiclesById);
            if (succeeded) 
            {
                return RedirectToAction(nameof(VehiclesIndex));
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
