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

    public async Task<int> CountVehicles()
    {
        return await _context.VehicleMake.CountAsync();
    }
    public async Task<List<VehicleMake>> GetVehicles(SortItems<VehicleMakeEntity> sortItems, FilterItems filterItems, PaginateItems<VehicleMakeEntity> paginateItems)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        vehicles = sortItems.sort(vehicles);
        vehicles = filterItems.filter(vehicles);
        vehicles = await paginateItems.paginate(vehicles);
        
        return _mapper.Map<List<VehicleMake>>(await vehicles.ToListAsync());
    }
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return _mapper.Map<VehicleMake>(await _context.VehicleMake.FindAsync(id));
    }
    public async Task<bool> CheckVehicleByName(string name)
    {
        return await _context.VehicleMake.AnyAsync(v => v.Name.Equals(name));
    }
    public async Task<bool> CheckVehicleById(int id)
    {
        return await _context.VehicleMake.AnyAsync(v => v.Id.Equals(id));
    }
    public async Task<VehicleMake?> GetVehicleByName(string name)
    {
        return _mapper.Map<VehicleMake>(await _context.VehicleMake.Where(v => v.Name == name).AsNoTracking().FirstOrDefaultAsync());
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