using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var app = WebApplication.Create(args);

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
app.Run();