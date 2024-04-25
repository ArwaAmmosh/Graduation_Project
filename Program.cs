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
global using Microsoft.AspNetCore.Authentication.JwtBearer; 
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Graduation_Project.Bases;
global using Graduation_Project.Features.Users.commands.Models;
global using Graduation_Project.Features.Users.Queries.Models;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using System.Drawing.Text;
global using Graduation_Project.Services.Abstracts;
global using Graduation_Project.Services.Implemention;
global using Graduation_Project.Service;
global using Microsoft.AspNetCore.Mvc.Infrastructure;
global using Microsoft.AspNetCore.Mvc.Routing;
global using Graduation_Project.Entities.Identity;
global using Graduation_Project.Infrastructure.Abstract;
global using Graduation_Project.Infrastructure.Repository;
global using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceRegisteration(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddDbContext<UNITOOLDbContext>();
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

builder.Services.AddServiceDependencies();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
