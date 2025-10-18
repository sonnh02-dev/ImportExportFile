using ImportExportFile.Application;
using ImportExportFile.Application.Abstractions;
using ImportExportFile.Application.Services;
using ImportExportFile.Domain.Entities;
using ImportExportFile.Infrastructure;
using ImportExportFile.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);




builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
