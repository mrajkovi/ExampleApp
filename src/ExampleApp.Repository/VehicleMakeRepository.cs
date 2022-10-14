using ExampleApp.Repository.Common;
using ExampleApp.Model.Common;
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

    public async Task<List<IVehicleMake>> GetVehicles(QueryModifier queryModifier)
    {
        var vehicles = _context.VehicleMake.AsQueryable();
        vehicles = new SortItems(queryModifier.SortOrder).sort(vehicles);
        vehicles = new FilterItems(queryModifier.SearchString).filter(vehicles);
        vehicles = await new PaginateItems<VehicleMakeEntity>(queryModifier.PageNumber, queryModifier.PageSize).paginate(vehicles);
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            return _mapper.Map<List<IVehicleMake>>(await vehicles.ToListAsync());
        }
        catch
        {
            return new List<IVehicleMake>();
        }
        
    }
    
    public async Task<IVehicleMake?> GetVehicleById(int id)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            return _mapper.Map<IVehicleMake>(await _context.VehicleMake.FindAsync(id));
        }
        catch
        {
            return null;
        }
    }

    public async Task<IVehicleMake?> GetVehicleByName(string name)
    {
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
        // services will handle bad results in that case
        try
        {
            return _mapper.Map<IVehicleMake>(await _context.VehicleMake.Where(v => v.Name == name).FirstOrDefaultAsync());
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> CreateVehicle(IVehicleMake vehicle) 
    {
        _context.VehicleMake.Add(_mapper.Map<VehicleMakeEntity>(vehicle));

        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
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

    public async Task<bool> UpdateVehicle(IVehicleMake newVehicle, IVehicleMake oldVehicle)
    {
        
        oldVehicle.Abbrv = newVehicle.Abbrv;
        oldVehicle.Name = newVehicle.Name;

        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
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

    public async Task<bool> DeleteVehicle(IVehicleMake vehicle)
    {
        _context.VehicleMake.Remove(_mapper.Map<VehicleMakeEntity>(_mapper.Map<VehicleMakeEntity>(vehicle)));
        // we will keep it simple with try catch expression; we will assume that if something is wrong then it's our fault
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