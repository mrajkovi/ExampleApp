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
    public async Task<int> CountVehiclesModels()
    {
        return await _context.VehicleModel.CountAsync();
    }
    public async Task<List<VehicleModel>> GetVehiclesModels(QueryDataSFP queryDataSFP)
    {
        var models = _context.VehicleModel.AsQueryable();
        models = new SortItems<VehicleModelEntity>(queryDataSFP.SortOrder).sort(models);
        models = new FilterItems(queryDataSFP.SearchString).filter(models);
        models = await new PaginateItems<VehicleModelEntity>(queryDataSFP.PageNumber, queryDataSFP.PageSize).paginate(models);
        
        return _mapper.Map<List<VehicleModel>>(await models.ToListAsync<VehicleModelEntity>());
    } 
    public async Task<VehicleModel?> GetVehicleModelByName(string name)
    {
        return _mapper.Map<VehicleModel>(await _context.VehicleModel.Where(m => m.Name == name).AsNoTracking().FirstOrDefaultAsync());
    }
    public async Task<VehicleModel?> GetVehicleModelById(int id)
    {
        return _mapper.Map<VehicleModel>(await _context.VehicleModel.FindAsync(id));
    }
    public async Task<bool> CheckVehicleModelById(int id)
    {
        return await _context.VehicleModel.AnyAsync(v => v.Id.Equals(id));
    }
    public async Task<bool> CheckVehicleModelByName(string name)
    {
        return await _context.VehicleModel.AnyAsync(v => v.Name.Equals(name));
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