using HouseHistory.Dependencies;
using HouseHistory.Routes.Auth;
using HouseHistory.Middleware;
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
      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
      });
      c.OperationFilter<CustomHeaderSwaggerAttribute>();
      c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
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

app.UseWhen(
    context => !context.Request.Path.StartsWithSegments("/api/v1/auth"),
    app =>
    {
      app.UseSupabaseSession();
    }
);


app.MapGet("/", () => "Hello frens!");
app.RegisterAuthRoutes();
app.RegisterHousesRoutes();

app.Run();