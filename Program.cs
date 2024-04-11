global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using Graduation_Project.Entities;
using Graduation_Project.Helpers;
using System.Configuration;
using Microsoft.Extensions.Hosting;
using Graduation_Project.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceRegisteration();
builder.Services.AddDbContext<UNITOOLDbContext>();
builder.Services.Configure <JWT> (builder.Configuration.GetSection("JWT"));



var app = builder.Build();

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
