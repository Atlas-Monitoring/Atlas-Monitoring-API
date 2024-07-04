using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Infrastructure.DataLayers;
using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add Cointrollers to API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

//Add database connection
builder.Services.AddDbContext<DefaultDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultContext"), serverVersion: ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultContext")));
});

//Scope DataLayer interface
builder.Services.AddScoped<IComputerDataLayer, ComputerDataLayer>();

//Scope Repository interface
builder.Services.AddScoped<IComputerRepository, IComputerRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.MapControllers();

app.Run();
