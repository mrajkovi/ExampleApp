using ExampleApp.Repository.Common;
using ExampleApp.Model;
using ExampleApp.Common;
using ExampleApp.DAL;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Repository;
public class VehicleMakeRepository : IVehicleMakeRepository
{
    private readonly ExampleAppContext _context = null!;
    private readonly IMapper _mapper;
    public VehicleMakeRepository(ExampleAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> CountVehicles(FilterItems filterVehicles)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        vehicles = filterVehicles.Find(vehicles).OfType<VehicleMakeEntity>();
        return await vehicles.CountAsync();
    }
    public async Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems filterItems, PaginateItems<VehicleMakeEntity> paginateItems)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        vehicles = sortItems.Sort(vehicles);
        vehicles = filterItems.Find(vehicles).OfType<VehicleMakeEntity>();
        vehicles = await paginateItems.Paginate(vehicles);
        
        return _mapper.Map<List<VehicleMake>>(await vehicles.ToListAsync());
    }
    public async Task<bool> CheckVehicle(FilterItems filterItems)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        vehicles = filterItems.Find(vehicles).OfType<VehicleMakeEntity>();
        
        return await vehicles.AnyAsync();
    }
    public async Task<VehicleMake?> GetVehicle(FilterItems filterItems)
    {
        var vehicles = _context.VehicleMake.AsQueryable().AsNoTracking();
        vehicles = filterItems.Find(vehicles).OfType<VehicleMakeEntity>();
        
        return _mapper.Map<VehicleMake>(await vehicles.FirstOrDefaultAsync());
    }
    public async Task CreateVehicle(VehicleMake vehicle) 
    {
        _context.VehicleMake.Add(_mapper.Map<VehicleMakeEntity>(vehicle));
        
        await _context.SaveChangesAsync();
    }
    public async Task UpdateVehicle(VehicleMake newVehicle)
    {
        _context.Update(_mapper.Map<VehicleMakeEntity>(newVehicle));

        await _context.SaveChangesAsync();
    }
    public async Task DeleteVehicle(VehicleMake vehicle)
    {
        _context.VehicleMake.Remove(_mapper.Map<VehicleMakeEntity>(vehicle));

        await _context.SaveChangesAsync();
    }
}