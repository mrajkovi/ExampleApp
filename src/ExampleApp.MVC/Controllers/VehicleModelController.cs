using Microsoft.AspNetCore.Mvc;
using ExampleApp.Service.Common;
using ExampleApp.Common;
using ExampleApp.Model;
using ExampleApp.MVC.ViewModels;
using ExampleApp.MVC.Common;
using ExampleApp.DAL;
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
    public async Task<IActionResult> ModelsIndex(QueryDataManipulation queryDataManipulation)
    {
        _logger.LogInformation(EventID.GetItems, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        
        try
        {
            var sortItems = new SortItems<VehicleModelEntity>(queryDataManipulation.SortOrder);
            FilterItems filterModels;
            
            if (queryDataManipulation.SearchByNumber)
            {
                filterModels = new FilterItems(queryDataManipulation.SearchString, "makeId");
            }
            else
            {
                filterModels = new FilterItems(queryDataManipulation.SearchString, "name_abbrv");
            }

            var paginateItems = new PaginateItems<VehicleModelEntity>(queryDataManipulation.PageNumber, queryDataManipulation.PageSize);

            List<VehicleModel> models = await _service.GetVehiclesModels(sortItems, filterModels, paginateItems);

            DataManipulationViewModel dataManipulationViewModel = _mapper.Map<DataManipulationViewModel>(queryDataManipulation);
            dataManipulationViewModel.TotalSize = await _service.CountVehiclesModels(filterModels);
            VehiclesModelsViewModel vehiclesModelsViewModel = new VehiclesModelsViewModel();
            vehiclesModelsViewModel.Models = _mapper.Map<List<VehicleModelViewModel>>(models);
            VehiclesModelsIndexViewModel vehiclesModelsIndexViewModel = new VehiclesModelsIndexViewModel(vehiclesModelsViewModel, dataManipulationViewModel);

            return View("Index", vehiclesModelsIndexViewModel);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            
            return View("Error");
        }
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetVehicleModel(int id)
    {
        _logger.LogInformation(EventID.GetItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        
        try
        {
            var filterModelsById = new FilterItems(id.ToString(), "id");
            
            VehicleModel? vehicleModel = await _service.GetVehicleModel(filterModelsById);
            
            if (vehicleModel == null) 
            {
                _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                
                return NotFound();
            }
            
            return Ok(vehicleModel);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateVehicleModel(VehicleModelViewModel vehicleModelViewModel)
    {
        _logger.LogInformation(EventID.InsertItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        
        try
        {
            VehicleModel vehicleModel = _mapper.Map<VehicleModel>(vehicleModelViewModel);
            
            bool succeeded = await _service.CreateVehicleModel(vehicleModel);

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
    [HttpPost("update/{id:int}")]
    public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelViewModel vehicleModelsViewModel)
    {
        _logger.LogInformation(EventID.UpdateItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        
        try
        {
            var filterModelsById = new FilterItems(id.ToString(), "id");
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel = _mapper.Map<VehicleModel>(vehicleModelsViewModel);
            
            bool succeeded = await _service.UpdateVehicleModel(filterModelsById, vehicleModel);
            
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
            var filterModelsById = new FilterItems(id.ToString(), "id");
            VehicleModel? vehicleModel = await _service.GetVehicleModel(filterModelsById);
            
            if (vehicleModel == null)
            {
                _logger.LogWarning(EventID.GetItemNotFound, "Item not found");
                
                return RedirectToAction(nameof(ModelsIndex));
            }
            VehicleModelViewModel vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);
            return View("Update", vehicleModelViewModel);
        }
        catch(Exception exception)
        {
            _logger.LogError(EventID.InternalError, exception, "Exception occurred");
            
            return View("Error");
        }
    }
    [HttpPost("delete/{id:int}")]
    public async Task<IActionResult> DeleteVehicleModel(int id)
    {
        _logger.LogInformation(EventID.DeleteItem, ControllerContext.HttpContext.GetEndpoint()?.ToString());
        try
        {
            var filterModelsById = new FilterItems(id.ToString(), "id");
            bool succeeded = await _service.DeleteVehicleModel(filterModelsById);
            
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
