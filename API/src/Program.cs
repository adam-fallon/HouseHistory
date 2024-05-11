using HouseHistory.Dependencies;
using HouseHistory.Routes.Auth;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var app = WebApplication.Create(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Config
builder
  .Configuration
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{app.Environment.EnvironmentName}.json", optional: true)
  .AddEnvironmentVariables();

if (app.Environment.IsDevelopment())
{
  foreach (var c in builder.Configuration.AsEnumerable())
  {
    Console.WriteLine(c.Key + " = " + c.Value);
  }
}


// Dependencies
builder
  .Services
  .AddControllers();

builder
  .Services
  .AddSingleton<ISupabaseService, SupabaseServiceImpl>();

builder.Services.AddEndpointsApiExplorer();
if (app.Environment.IsDevelopment())
{
  builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "House History API", Description = "Keep track of your houses", Version = "v1" });
    });

  app = builder.Build();
  app.UseSwagger();
  app.UseSwaggerUI(c =>
   {
     c.SwaggerEndpoint("/swagger/v1/swagger.json", "House History API V1");
   });
}
else
{
  app = builder.Build();
}

app.UseRouting();
app.MapGet("/", () => "Hello frens!");
app.RegisterAuthRoutes();

app.Run();