namespace PortFreight.Infrastructure.Repositories.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    Task SaveChangesAsync();
}
