global using Graduation_Project.Entities;
global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations;
global using Graduation_Project.Helpers;
global using Graduation_Project.Services;
global using Microsoft.AspNetCore.Localization;
global using System.Globalization;
global using MediatR;
global using AutoMapper;
global using System.Net;
global using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceRegisteration();
builder.Services.AddDbContext<UNITOOLDbContext>();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt => opt.ResourcesPath = "");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCulture = new List<CultureInfo> {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("ar-EG"),
        new CultureInfo("de-DE")

    };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCulture;
    options.SupportedUICultures = supportedCulture;
});
#endregion


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
