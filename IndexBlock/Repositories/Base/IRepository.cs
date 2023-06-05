namespace IndexBlock.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<int> SaveChangesAsync();
    }
}
