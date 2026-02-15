namespace PortFreight.MVC.Services.Implementation;

public class PortFreightClient(HttpClient httpClient) : IPortFreightClient
{
    public async Task<IEnumerable<ShipmentViewModel>> GetShipmentsAsync()
    {
        var response = await httpClient.GetAsync("api/shipments");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ShipmentViewModel>>() ?? Enumerable.Empty<ShipmentViewModel>();
    }


    public async Task<ShipmentViewModel?> GetShipmentAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"api/shipments/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ShipmentViewModel>() ?? null;
    }

    public async Task<ShipmentViewModel> CreateShipmentAsync(CreateShipmentViewModel viewModel)
    {
        var response = await httpClient.PostAsJsonAsync("api/shipments", viewModel);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ShipmentViewModel>() ?? throw new Exception("Failed to create shipment");
    }

    public async Task UpdateShipmentAsync(Guid id, UpdateShipmentViewModel viewModel)
    {
        var response = await httpClient.PutAsJsonAsync($"api/shipments/{id}", viewModel);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteShipmentAsync(Guid id)
    {
        var response = await httpClient.DeleteAsync($"api/shipments/{id}");
        response.EnsureSuccessStatusCode();
    }
}