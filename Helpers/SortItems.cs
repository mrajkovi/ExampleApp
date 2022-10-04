using ExampleApp.Models;

namespace ExampleApp.Helpers.Sort;
public class SortItems
{
    public string SortOrder { get; private set; }
    public SortItems(string? sortOrder) 
    {
        if (sortOrder != null)
        {
            SortOrder = sortOrder;
        }
        else
        {
            SortOrder = "";
        }
    }
    public IQueryable<VehicleMake> sort(IQueryable<VehicleMake> items)
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
    public IQueryable<VehicleModel> sort(IQueryable<VehicleModel> items)
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