var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PortFreightDatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PortFreightDatabase");
    var assemblyName = typeof(PortFreightDatabaseContext).Assembly.FullName;
    options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName));
});

builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IVesselService, VesselService>();
builder.Services.AddScoped<IPortService, PortService>();
builder.Services.AddScoped<IContainerService, ContainerService>();
builder.Services.AddScoped<ITrackingEventService, TrackingEventService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidationFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseGlobalExceptionMiddleware();
app.UseMiddleware<ValidationMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
