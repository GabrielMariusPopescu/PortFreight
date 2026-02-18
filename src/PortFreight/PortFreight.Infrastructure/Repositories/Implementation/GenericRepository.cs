namespace PortFreight.Infrastructure.Repositories.Implementation;

public class GenericRepository<T>(PortFreightDatabaseContext context) : IGenericRepository<T>
    where T : class
{
    protected readonly PortFreightDatabaseContext Context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id) =>
        await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    public async Task AddAsync(T entity) =>
        await _dbSet.AddAsync(entity);

    public bool Update(T entity)
    {
        var updated = _dbSet.Update(entity);
        var state = updated.State;
        return state == EntityState.Modified;
    }

    public bool Delete(T entity)
    {
        var deleted = _dbSet.Remove(entity);
        var state = deleted.State;
        return state == EntityState.Deleted;
    }

    public async Task SaveChangesAsync() =>
        await Context.SaveChangesAsync();
}

