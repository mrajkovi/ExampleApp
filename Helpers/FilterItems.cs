using ExampleApp.Models;

namespace ExampleApp.Helpers.Filter;
public class FilterItems
{
    public string _filter;
    public FilterItems(string filter) 
    {
        _filter = filter;
    }

    public IQueryable<VehicleMake> filter(IQueryable<VehicleMake> items)
    {
        return items.Where(v => v.Name.Contains(_filter) || v.Abbrv.Contains(_filter));
    }

    public IQueryable<VehicleModel> filter(IQueryable<VehicleModel> items)
    {
        return items.Where(v => v.Name.Contains(_filter) || v.Abbrv.Contains(_filter));
    }
}