using ExampleApp.DAL;

namespace ExampleApp.Common;

public class FilterItems
{
    public string FilterString { get; private set; }
    private string FilterBy { get; set; }
    private bool FilterExactly { get; set; }
    public FilterItems(string? filter, string filterBy, bool filterExactly = false) 
    {
        if (!string.IsNullOrEmpty(filter))
        {
            FilterString = filter.ToLower();
        } 
        else 
        {
            FilterString = "";
        }

        switch (filterBy)
        {
            case "name":
                FilterBy = "name";
                break;
            case "name_abbrv":
                FilterBy = "name_abbrv";
                break;
            case "abbrv":
                FilterBy = "abbrv";
                break;
            case "makeId":
                FilterBy = "makeId";
                break;
            default:
                FilterBy = "id";
                break;
        }

        FilterExactly = filterExactly; 
    }
    private IQueryable<IVehicleBaseEntity> FindCommon(IQueryable<IVehicleBaseEntity> items)
    {
        if (!string.IsNullOrWhiteSpace(FilterString))
        {
            if (!FilterExactly)
            {
                switch (FilterBy)
                {
                    case "name":
                        items = items.Where(v => v.Name.ToLower().Contains(FilterString));
                        break;
                    case "abbrv":
                        items = items.Where(v => v.Name.ToLower().Contains(FilterString));
                        break;
                    case "name_abbrv":
                        items = items.Where(v => v.Name.ToLower().Contains(FilterString) || v.Abbrv.ToLower().Contains(FilterString));
                        break;
                    case "id":
                        if (Int32.TryParse(FilterString, out int id))
                        {
                            items = items.Where(v => v.Id == id);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (FilterBy)
                {
                    case "name":
                        items = items.Where(v => v.Name.ToLower().Equals(FilterString));
                        break;
                    case "abbrv":
                        items = items.Where(v => v.Abbrv.ToLower().Equals(FilterString));
                        break;
                    case "name_abbrv":
                        items = items.Where(v => v.Name.ToLower().Equals(FilterString) || v.Abbrv.ToLower().Equals(FilterString));
                        break;
                    case "id":
                        if (Int32.TryParse(FilterString, out int id))
                        {
                            items = items.Where(v => v.Id == id);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        return items;
    }
    public IQueryable<VehicleMakeEntity> FindVehicles(IQueryable<VehicleMakeEntity> items)
    {
        return FindCommon(items).OfType<VehicleMakeEntity>();
    }
    public IQueryable<VehicleModelEntity> FindModels(IQueryable<VehicleModelEntity> items)
    {
        if (FilterBy.Equals("makeId") && Int32.TryParse(FilterString, out int makeId))
        {
            items = items.Where(m => m.MakeId == makeId);
        }
        else
        {
            items = FindCommon(items).OfType<VehicleModelEntity>();
        }
        return items;
    }
}