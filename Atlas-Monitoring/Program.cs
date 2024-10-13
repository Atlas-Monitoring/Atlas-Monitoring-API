using Atlas_Monitoring.Core.Application.Repositories;
using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Infrastructure.DataLayers;
using Atlas_Monitoring.Core.Interface.Application;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add Serilog
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

//Add Cointrollers to API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = document =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "Atlas API";
        document.Info.Description = "API of Atlas application";
        document.Info.Contact = new OpenApiContact
        {
            Name = "ATLAS",
            Email = string.Empty,
        };
    };

    config.AddSecurity("JWT Token", Enumerable.Empty<string>(),
        new OpenApiSecurityScheme()
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copy your Token : Bearer {token}"
        }
    );
});

builder.Services.AddAuthentication(cfg => {
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(builder.Environment.IsDevelopment() ? "6wE3sLgJQx42BKm9966hHwxS6wE3sLgJQx42BKm9966hHwxS6wE3sLgJQx42BKm9966hHwxS" : Environment.GetEnvironmentVariable("JWT_TOKEN"))
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

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
builder.Services.AddScoped<IEntityDataLayer, EntityDataLayer>();
builder.Services.AddScoped<IUserDataLayer, UserDataLayer>();

//Scope Repository interface
builder.Services.AddScoped<IComputerDataRepository, ComputerDataRepository>();
builder.Services.AddScoped<IComputerHardDriveRepository, ComputerHardDriveRepository>();
builder.Services.AddScoped<IComputerPartsRepository, ComputerPartsRepository>();
builder.Services.AddScoped<IComputerRepository, ComputerRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceSoftwareInstalledRepository, DeviceSoftwareInstalledRepository>();
builder.Services.AddScoped<IEntityRepository, EntityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || Convert.ToBoolean(Environment.GetEnvironmentVariable("ENABLE_SWAGGER")))
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseAuthentication();
app.UseAuthorization();
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
