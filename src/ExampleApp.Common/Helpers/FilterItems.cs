using ExampleApp.DAL;

namespace ExampleApp.Common;

public class FilterItems
{
    public string FilterString { get; private set; }
    public FilterItems(string? filter) 
    {
        if (!string.IsNullOrEmpty(filter))
        {
            FilterString = filter;
        } else 
        {
            FilterString = "";
        }
    }
    public IQueryable<VehicleMakeEntity> filter(IQueryable<VehicleMakeEntity> items)
    {
        if (FilterString != "")
        {
            return items.Where(v => v.Name.Contains(FilterString) || v.Abbrv.Contains(FilterString));
        }
        else 
        {
            return items;
        }
    }
    public IQueryable<VehicleModelEntity> filter(IQueryable<VehicleModelEntity> items)
    {
        if (FilterString != "")
        {
            if (Int32.TryParse(FilterString, out int result))
            {
                items = items.Where(m => m.MakeId == result);
            } else 
            {
                items = items.Where(m => m.Name.Contains(FilterString) || m.Abbrv.Contains(FilterString));
            }
            return items;
        }
        else 
        {
            return items;
        }
    }
}