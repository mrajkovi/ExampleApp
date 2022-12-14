using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Common;

public class PaginateItems<T>
{
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public int TotalSize { get; private set; }
    public PaginateItems(string? pageNumber, string? pageSize)
    {
        if (Int32.TryParse(pageNumber, out int pageNumberResult) && Int32.TryParse(pageSize, out int pageSizeResult))
        {
            PageNumber = pageNumberResult;
            PageSize = pageSizeResult;
        } 
        else
        {
            PageNumber = 1;
            PageSize = 5;
        }
    }
    public async Task<IQueryable<T>> Paginate(IQueryable<T> source)
    {
        TotalSize = await source.CountAsync();
        var items = source.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        return items;
    }
}