namespace PortFreight.MVC.Services;

public class ApiClient(IHttpClientFactory factory, IHttpContextAccessor context)
{
    public async Task<T?> GetAsync<T>(string url)
    {
        var token = context.HttpContext!.User.FindFirst("JWT")?.Value;

        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await client.GetFromJsonAsync<T>(url);
    }
}