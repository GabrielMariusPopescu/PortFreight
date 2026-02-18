var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IPortFreightClient, PortFreightClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7014");
    var value = new MediaTypeWithQualityHeaderValue("application/json");
    client.DefaultRequestHeaders.Accept.Add(value);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ApiClient>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("PortFreightCookie")
    .AddCookie("PortFreightCookie", options =>
    {
        options.LoginPath = "/api/Auth/Login";
        options.LogoutPath = "/api/Auth/Logout";
    });

builder.Services.AddAuthorization();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
