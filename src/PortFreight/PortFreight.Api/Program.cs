var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PortFreightIdentity");
    var assemblyName = typeof(IdentityDatabaseContext).Assembly.FullName;
    options.UseSqlServer(connectionString, 
        optionsBuilder => 
            optionsBuilder.MigrationsAssembly(assemblyName));
});

var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = jwt["Key"]!;
        var bytes = Encoding.UTF8.GetBytes(key);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(bytes),
            ClockSkew = TimeSpan.FromMinutes(5),
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<IdentityDatabaseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<PortFreightDatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PortFreightDatabase");
    var assemblyName = typeof(PortFreightDatabaseContext).Assembly.FullName;
    options.UseSqlServer(connectionString, 
        optionsBuilder => 
            optionsBuilder.MigrationsAssembly(assemblyName));
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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Port Freight API",
        Version = "v1",
        Description = "API documentation for the Port Freight system."
    });

    // Enable XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

app.UseCorrelationIdMiddleware();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseGlobalExceptionMiddleware();
app.UseMiddleware<ValidationMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
