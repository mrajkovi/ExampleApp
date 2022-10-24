using ExampleApp.Repository.Common;
using ExampleApp.DAL;
using ExampleApp.Model;
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
    public async Task<int> CountVehiclesModels(FilterItems filterModels)
    {
        var models = _context.VehicleModel.AsQueryable();
        models = filterModels.Find(models).OfType<VehicleModelEntity>();
        return await models.CountAsync();
    }
    public async Task<List<VehicleModel>> GetVehiclesModels(SortItems<VehicleModelEntity> sortItems, FilterItems filterItems, PaginateItems<VehicleModelEntity> paginateItems)
    {
        var models = _context.VehicleModel.AsQueryable();
        models = sortItems.Sort(models);
        models = filterItems.Find(models).OfType<VehicleModelEntity>();
        models = await paginateItems.Paginate(models);
        
        return _mapper.Map<List<VehicleModel>>(await models.ToListAsync<VehicleModelEntity>());
    } 
    public async Task<VehicleModel?> GetVehicleModel(FilterItems filterItems)
    {
        var models = _context.VehicleModel.AsQueryable().AsNoTracking();
        models = filterItems.Find(models).OfType<VehicleModelEntity>();
        
        return _mapper.Map<VehicleModel>(await models.FirstOrDefaultAsync());
    }
    public async Task<bool> CheckVehicleModel(FilterItems filterItems)
    {
        var models = _context.VehicleModel.AsQueryable();
        models = filterItems.Find(models).OfType<VehicleModelEntity>();
        
        return await models.AnyAsync();
    }
    public async Task CreateVehicleModel(VehicleModel vehicleModel) 
    {
        _context.VehicleModel.Add(_mapper.Map<VehicleModelEntity>(vehicleModel));
        
        await _context.SaveChangesAsync();
    }
    public async Task UpdateVehicleModel(VehicleModel updatedModel)
    {
        _context.VehicleModel.Update(_mapper.Map<VehicleModelEntity>(updatedModel));

        await _context.SaveChangesAsync();
    }
    public async Task DeleteVehicleModel(VehicleModel model)
    {
        _context.VehicleModel.Remove(_mapper.Map<VehicleModelEntity>(model));
        
        await _context.SaveChangesAsync();
    }
}