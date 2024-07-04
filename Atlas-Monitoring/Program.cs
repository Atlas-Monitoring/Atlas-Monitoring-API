using Atlas_Monitoring.Core.Infrastructure.DataBases;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Add OpenAPI 3.0 document serving middleware
    // Available at: http://localhost:<port>/swagger/v1/swagger.json
    app.UseOpenApi();

    // Add web UIs to interact with the document
    // Available at: http://localhost:<port>/swagger
    app.UseSwaggerUi(); // UseSwaggerUI Protected by if (env.IsDevelopment())
}

app.MapGet("/", () => "Hello World !");
app.MapControllers();

app.Run();
