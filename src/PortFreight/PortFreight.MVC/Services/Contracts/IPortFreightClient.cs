namespace PortFreight.MVC.Services.Contracts;

public interface IPortFreightClient
{
    Task<IEnumerable<T>> GetAsync<T>(string url);

    Task<T?> GetAsync<T>(string url, Guid id);

    Task<T> CreateAsync<T>(string url, T viewModel);

    Task UpdateAsync<T>(string url, Guid id, T viewModel);

    Task DeleteAsync<T>(string url,Guid id);
}