using Microsoft.EntityFrameworkCore;
using PortFreight.Infrastructure;
using PortFreight.Infrastructure.Repositories.Contracts;
using PortFreight.Infrastructure.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PortFreightDatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PortFreightDatabase");
    var assemblyName = typeof(PortFreightDatabaseContext).Assembly.FullName;
    options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
