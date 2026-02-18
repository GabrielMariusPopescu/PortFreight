namespace PortFreight.MVC.Controllers;

public class AuthController(IHttpClientFactory factory) : Controller
{
    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var client = factory.CreateClient();
        client.BaseAddress = new Uri("https://localhost:7014");
        var response = await client.PostAsJsonAsync("api/Auth/register", model);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Login");

        ModelState.AddModelError("", "Registration failed");
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var client = factory.CreateClient();
        client.BaseAddress = new Uri("https://localhost:7014");
        var response = await client.PostAsJsonAsync("api/Auth/login", model);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Invalid login");
            return View(model);
        }

        var json = await response.Content.ReadAsStringAsync();
        var token = JsonDocument.Parse(json).RootElement.GetProperty("token").GetString();

        var claims = new List<Claim>
        {
            new("JWT", token!)
        };

        var identity = new ClaimsIdentity(claims, "PortFreightCookie");
        await HttpContext.SignInAsync("PortFreightCookie", new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("PortFreightCookie");
        return RedirectToAction("Login");
    }
}
