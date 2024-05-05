using HouseHistory.Dependencies;
using HouseHistory.Models.Requests;
using Microsoft.OpenApi.Models;
using Supabase.Gotrue;

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


app.MapGet("/", () => "Hello frens!");

app.MapPost($"/api/v1/auth/signup", async (ISupabaseService supabaseService, SignUpRequest signUpRequest) =>
{
  var supabase = app.Services.GetRequiredService<ISupabaseService>();
  var client = await supabase.GetClient();
  var options = new SignUpOptions
  {
    Data = new Dictionary<string, object>
    {
      { "username", signUpRequest.Username }
    }
  };

  var result = await client
    .Auth
    .SignUp(signUpRequest.Email, signUpRequest.Password, options);

  return result;
});

app.Run();