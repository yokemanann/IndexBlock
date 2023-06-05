namespace IndexBlock.Repositories.Base
{
    public class Repository<T> : IRepository<T> 
        where T : class
    {
        protected readonly IndexBlock_DBContext _dbContext;

        public Repository(IndexBlock_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await _dbContext.AddAsync(entity)).Entity;
        }
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
