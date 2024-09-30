using Atlas_Monitoring.Core.Application.Repositories;
using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Infrastructure.DataLayers;
using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add Serilog
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

//Add Cointrollers to API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

//Connection string
string connectionString = string.Empty;

if (!builder.Environment.IsDevelopment())
{
    connectionString = $"server={Environment.GetEnvironmentVariable("DB_SERVER")};user={Environment.GetEnvironmentVariable("DB_USER")};password={Environment.GetEnvironmentVariable("DB_PASSWORD")};database={Environment.GetEnvironmentVariable("DB_DATABASE")}";
}
else
{
    connectionString = "server=localhost;user=root;password=toor;database=atlas-monitoring";
}

//Add database connection
builder.Services.AddDbContext<DefaultDbContext>(options =>
{
    options.UseMySql(connectionString, serverVersion: ServerVersion.AutoDetect(connectionString));
});

//Scope DataLayer interface
builder.Services.AddScoped<IComputerDataDataLayer, ComputerDataDataLayer>();
builder.Services.AddScoped<IComputerDataLayer, ComputerDataLayer>();
builder.Services.AddScoped<IComputerPartsDataLayer, ComputerPartsDataLayer>();
builder.Services.AddScoped<IComputerHardDriveDataLayer, ComputerHardDriveDataLayer>();
builder.Services.AddScoped<IDeviceDataLayer, DeviceDataLayer>();
builder.Services.AddScoped<IDeviceSoftwareInstalledDataLayer, DeviceSoftwareInstalledDataLayer>();

//Scope Repository interface
builder.Services.AddScoped<IComputerDataRepository, ComputerDataRepository>();
builder.Services.AddScoped<IComputerHardDriveRepository, ComputerHardDriveRepository>();
builder.Services.AddScoped<IComputerPartsRepository, ComputerPartsRepository>();
builder.Services.AddScoped<IComputerRepository, ComputerRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceSoftwareInstalledRepository, DeviceSoftwareInstalledRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || Convert.ToBoolean(Environment.GetEnvironmentVariable("ENABLE_SWAGGER")))
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.MapControllers();
ApplyMigration();

app.Run();

//Apply migration on startup of app
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _Db = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();
        if (_Db != null)
        {
            if (_Db.Database.GetPendingMigrations().Any())
            {
                _Db.Database.Migrate();
            }
        }
    }
}
