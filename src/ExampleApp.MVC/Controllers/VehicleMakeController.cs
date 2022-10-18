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
    public async Task<IActionResult> VehiclesIndex(QueryDataSFP queryDataSFP)
    {
        _logger.LogInformation(EventID.GetItems, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try 
        {
            var sortItems = new SortItems<VehicleMakeEntity>(queryDataSFP.SortOrder);
            FilterItems filterVehicles;
            if (queryDataSFP.SearchByNumber)
            {
                filterVehicles = new FilterItems(queryDataSFP.SearchString, "id");
            }
            else
            {
                filterVehicles = new FilterItems(queryDataSFP.SearchString, "name_abbrv");
            }
            var paginateItems = new PaginateItems<VehicleMakeEntity>(queryDataSFP.PageNumber, queryDataSFP.PageSize);
            List<VehicleMake> vehicles = await _service.GetVehicles(sortItems, filterVehicles, paginateItems);
            VehiclesPaginationViewModel vehiclesPaginationViewModel = _mapper.Map<VehiclesPaginationViewModel>(queryDataSFP);
            vehiclesPaginationViewModel.Vehicles = vehicles;
            vehiclesPaginationViewModel.TotalSize = await _service.CountVehicles();

            return View("Index", vehiclesPaginationViewModel);
        }
        catch (Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            return View("Error"); 
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicle(int id)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            var filterVehiclebyId = new FilterItems(id.ToString(), "id");
            VehicleMake? vehicle = await _service.GetVehicle(filterVehiclebyId);
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
    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetVehicle(string name)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try 
        {
            FilterItems filterVehiclesByNameOrAbbrv = new FilterItems(name, "name_abbrv");
            VehicleMake? vehicle = await _service.GetVehicle(filterVehiclesByNameOrAbbrv);
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
    public async Task<IActionResult> CreateVehicle(VehiclesPaginationViewModel vehiclesPaginationViewModel)
    {
        _logger.LogInformation(EventID.InsertItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            VehicleMake newVehicle = _mapper.Map<VehicleMake>(vehiclesPaginationViewModel);
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
    [HttpPost("update/{id}")]
    public async Task<IActionResult> UpdateVehicle(int id, VehicleViewModel vehicleViewModel)
    {
        _logger.LogInformation(EventID.UpdateItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try 
        {
            var filterVehiclesById = new FilterItems(id.ToString(), "id");
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
    [HttpGet("update/{id}")]
    public async Task<IActionResult> UpdateView(int id)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            var filterVehiclesById = new FilterItems(id.ToString(), "id");
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
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteVehicle(int id)
    {
        _logger.LogInformation(EventID.DeleteItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            var filterVehiclesById = new FilterItems(id.ToString(), "id");
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
