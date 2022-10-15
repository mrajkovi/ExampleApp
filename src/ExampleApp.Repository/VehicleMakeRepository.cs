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
    public async Task<List<VehicleMake>> GetVehicles(QueryDataSFP queryDataSFP)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        vehicles = new SortItems(queryDataSFP.SortOrder).sort(vehicles);
        vehicles = new FilterItems(queryDataSFP.SearchString).filter(vehicles);
        vehicles = await new PaginateItems<VehicleMakeEntity>(queryDataSFP.PageNumber, queryDataSFP.PageSize).paginate(vehicles);
        
        return _mapper.Map<List<VehicleMake>>(await vehicles.ToListAsync());
    }
    public async Task<VehicleMake?> GetVehicleById(int id)
    {
        return _mapper.Map<VehicleMake>(await _context.VehicleMake.FindAsync(id));
    }
    public async Task<VehicleMake?> GetVehicleByName(string name)
    {
        return _mapper.Map<VehicleMake>(await _context.VehicleMake.Where(v => v.Name == name).FirstOrDefaultAsync());
    }
    public async Task CreateVehicle(VehicleMake vehicle) 
    {
        _context.VehicleMake.Add(_mapper.Map<VehicleMakeEntity>(vehicle));
        
        await _context.SaveChangesAsync();
    }
    public async Task UpdateVehicle(VehicleMake newVehicle, VehicleMake oldVehicle)
    {
        oldVehicle.Abbrv = newVehicle.Abbrv;
        oldVehicle.Name = newVehicle.Name;
        
        await _context.SaveChangesAsync();
    }
    public async Task DeleteVehicle(VehicleMake vehicle)
    {
        _context.VehicleMake.Remove(_mapper.Map<VehicleMakeEntity>(_mapper.Map<VehicleMakeEntity>(vehicle)));

        await _context.SaveChangesAsync();
    }
}