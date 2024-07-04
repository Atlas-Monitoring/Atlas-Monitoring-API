using Atlas_Monitoring;
using Atlas_Monitoring.Core.Application.Repositories;
using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Infrastructure.DataLayers;
using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

//Add Serilog
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

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
builder.Services.AddScoped<IComputerRepository, ComputerRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
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
