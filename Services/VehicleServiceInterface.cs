using Microsoft.EntityFrameworkCore;
namespace ExampleApp.Interfaces.Services.Vehicle;
interface IVehicleServiceInterface<T>
{
    public class PaginatedList<K> : List<K>
    {
        public int pageNumber { get; private set; }
        public int totalPages { get; private set; }

        public PaginatedList(List<K> items, int count, int pageNumber, int pageSize)
        {
            this.pageNumber = pageNumber;
            totalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }

    public Task<List<T>> GetAll(string? sortOrder, string? searchString, string? pageNumber, string? pageSize);
    public Task<T?> GetById(int id);

    public Task<int> Create(T obj);
    public Task<int> Update(int id, T obj);

    public Task<int> Delete(int id);


}