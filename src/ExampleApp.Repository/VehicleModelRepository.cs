using ExampleApp.Repository.Common;
using ExampleApp.DAL;
using ExampleApp.Model.Common;
using ExampleApp.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Repository;

public class VehicleModelRepository : IVehicleModelRepository
{
    private readonly ExampleAppContext _context = null!;
    private readonly IMapper _mapper = null!;

    public VehicleModelRepository(ExampleAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<IVehicleModel>> GetVehiclesModels(QueryModifier queryModifier)
    {
        var models = _context.VehicleModel.AsQueryable();
        models = new SortItems(queryModifier.SortOrder).sort(models);
        models = new FilterItems(queryModifier.SearchString).filter(models);
        models = await new PaginateItems<VehicleModelEntity>(queryModifier.PageNumber, queryModifier.PageSize).paginate(models);
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try 
        {
            return _mapper.Map<List<IVehicleModel>>(await models.ToListAsync<VehicleModelEntity>());
        }
        catch 
        {
            return new List<IVehicleModel>();
        } 
    }
    
    public async Task<IVehicleModel?> GetVehicleModelByName(string name)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try
        {
            return _mapper.Map<IVehicleModel>(await _context.VehicleModel.Where(m => m.Name == name).FirstOrDefaultAsync());
        }
        catch
        {
            return null;
        }
    }
    
    public async Task<IVehicleModel?> GetVehicleModelById(int id)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try
        {
            return _mapper.Map<IVehicleModel>(await _context.VehicleModel.FindAsync(id));
        }
        catch
        {
            return null;
        }    
    }

    public async Task<bool> CreateVehicleModel(IVehicleModel vehicleModel) 
    {
        _context.VehicleModel.Add(_mapper.Map<VehicleModelEntity>(vehicleModel));
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateVehicleModel(IVehicleModel newModel, IVehicleModel oldModel)
    {
        
        oldModel.MakeId = newModel.MakeId;
        oldModel.Abbrv = newModel.Abbrv;
        oldModel.Name = newModel.Name;
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteVehicleModel(IVehicleModel model)
    {
        _context.VehicleModel.Remove(_mapper.Map<VehicleModelEntity>(model));
        // we will keep it simple with try catch expression; we will assume that if something is wrong then its our fault
        // services will handle bad results in that case
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}