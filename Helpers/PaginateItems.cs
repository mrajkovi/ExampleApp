namespace ExampleApp.Helpers.Paginate;
public class PaginateItems<T> : List<T>
{
    public int pageNumber { get; private set; }
    public int pageSize { get; private set; }

    public PaginateItems(int pageNumber, int pageSize)
    {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
    }

    public IQueryable<T> paginate(IQueryable<T> source)
    {
        // var count = await source.CountAsync();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        return items;
    }
}