using ExampleApp.Models;

namespace ExampleApp.Helpers.Filter;
public class FilterItems
{
    public string filterString { get; private set; }
    public FilterItems(string? filter) 
    {
        if (filter != null)
        {
            this.filterString = filter;
        } else 
        {
            this.filterString = "";
        }
    }

    public IQueryable<VehicleMake> filter(IQueryable<VehicleMake> items)
    {
        if (this.filterString != "")
        {
            return items.Where(v => v.Name.Contains(filterString) || v.Abbrv.Contains(filterString));
        }
        else 
        {
            return items;
        }
    }

    public IQueryable<VehicleModel> filter(IQueryable<VehicleModel> items)
    {
        if (this.filterString != "")
        {
            return items.Where(v => v.Name.Contains(filterString) || v.Abbrv.Contains(filterString));
        }
        else 
        {
            return items;
        }
    }
}