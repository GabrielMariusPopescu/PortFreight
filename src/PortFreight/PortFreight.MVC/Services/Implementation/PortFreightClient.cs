namespace PortFreight.MVC.Services.Implementation;

public class PortFreightClient(HttpClient httpClient) : IPortFreightClient
{
    public async Task<IEnumerable<T>> GetAsync<T>(string url)
    {
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<T>>() ?? Enumerable.Empty<T>();
    }

    public async Task<T?> GetAsync<T>(string url, Guid id)
    {
        var response = await httpClient.GetAsync($"{url}/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>() ?? default(T);
    }

    public async Task<T> CreateAsync<T>(string url, T viewModel)
    {
        var response = await httpClient.PostAsJsonAsync(url, viewModel);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>() ?? throw new Exception($"Failed to create {nameof(viewModel)}");
    }

    public async Task UpdateAsync<T>(string url, Guid id, T viewModel)
    {
        var response = await httpClient.PutAsJsonAsync($"{url}/{id}", viewModel);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync<T>(string url, Guid id)
    {
        var response = await httpClient.DeleteAsync($"{url}/{id}");
        response.EnsureSuccessStatusCode();
    }
}