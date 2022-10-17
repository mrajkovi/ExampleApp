using ExampleApp.DAL;

namespace ExampleApp.Common;

public class SortItems<T> where T : IVehicleBaseEntity
{
    public string SortOrder { get; private set; }
    public SortItems(string? sortOrder) 
    {
        if (!string.IsNullOrEmpty(sortOrder))
        {
            SortOrder = sortOrder;
        }
        else
        {
            SortOrder = "";
        }
    }
    public IQueryable<T> sort(IQueryable<T> items)
    { 
        switch (SortOrder)
        {   
            case "name_asc":
                items = items.OrderBy(v => v.Name);
                break;
            case "name_desc":
                items = items.OrderByDescending(v => v.Name);
                break;
            case "abbrv_asc":
                items = items.OrderBy(v => v.Abbrv);
                break;
            case "abbrv_desc":
                items = items.OrderByDescending(v => v.Abbrv);
                break;
            default:
                break;
        }
        return items;
    }
}