using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBC_Employee_API_MVC.Models;
using Serilog;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
    opts.UseSqlServer(builder.Configuration[
    "ConnectionStrings:EmployeeConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

// Setup Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/employee-api-mvc-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Following Line is to Test the Health and Functionality of the API
app.UseMiddleware<RBC_Employee_API_MVC.TestMiddleware>();

app.MapControllers();

app.MapGet("/", () => "Hello World!");
var context = app.Services.CreateScope().ServiceProvider
.GetRequiredService<DataContext>();
SeedData.SeedDatabase(context);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
