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
            case "name" :
            case "name_abbrv":
            case "abbrv":
            case "makeId":
                FilterBy = filterBy;
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
                        items = items.Where(v => v.Abbrv.ToLower().Contains(FilterString));
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

    public IQueryable<IVehicleBaseEntity> Find(IQueryable<IVehicleBaseEntity> items)
    {
        if (items is IQueryable<VehicleMakeEntity>)
        {
            return FindCommon(items);
        }
        else if (items is IQueryable<VehicleModelEntity>)
        {
            if (FilterBy.Equals("makeId") && Int32.TryParse(FilterString, out int makeId))
            {
                return items.OfType<VehicleModelEntity>().Where(m => m.MakeId == makeId);
            }
            else
            {
                return FindCommon(items);
            }
        }
        return items;
    }
}