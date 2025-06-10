using System.Reflection;
using APIForms.Extensions;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Add or inject services to the container.
builder.Services.ConfigureCors();
builder.Services.AddControllers();

builder.Services.AddAplicacionServices();

builder.Services.AddCustomRateLimiter();

builder.Services.AddDbContext<FormsContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// use services injected
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRateLimiter();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
