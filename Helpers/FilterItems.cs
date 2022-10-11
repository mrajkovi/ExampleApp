using ExampleApp.Models;

namespace ExampleApp.Helpers.Filter;
public class FilterItems
{
    public string FilterString { get; private set; }
    public FilterItems(string? filter) 
    {
        if (filter != null)
        {
            FilterString = filter;
        } else 
        {
            FilterString = "";
        }
    }

    public IQueryable<VehicleMake> filter(IQueryable<VehicleMake> items)
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

    public IQueryable<VehicleModel> filter(IQueryable<VehicleModel> items)
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