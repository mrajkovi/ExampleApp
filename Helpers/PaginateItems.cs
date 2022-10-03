namespace ExampleApp.Helpers.Paginate;
using Microsoft.EntityFrameworkCore;
public class PaginateItems<T> : List<T>
{
    public int pageNumber { get; private set; }
    public int pageSize { get; private set; }

    public int totalSize { get; private set; }

    public PaginateItems(string? pageNumber, string? pageSize)
    {
        if (Int32.TryParse(pageNumber, out int pageNumberResult) && Int32.TryParse(pageSize, out int pageSizeResult))
        {
            this.pageNumber = pageNumberResult;
            this.pageSize = pageSizeResult;
        } else
        {
            this.pageNumber = 1;
            this.pageSize = 5;
        }
    }

    public async Task<IQueryable<T>> paginate(IQueryable<T> source)
    {
        this.totalSize = await source.CountAsync();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return items;
    }
}