global using AutoMapper;
global using Graduation_Project.Bases;
global using Graduation_Project.Entities;
global using Graduation_Project.Entities.Identity;
global using Graduation_Project.Features.Users.commands.Models;
global using Graduation_Project.Features.Users.Queries.Models;
global using Graduation_Project.Features.Users.Queries.Result;
global using Graduation_Project.Helpers;
global using Graduation_Project.Resource;
global using Graduation_Project.Services;
global using Graduation_Project.Wrappers;
global using MediatR;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Localization;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Localization;
global using SharpDX.DXGI;
global using System.ComponentModel.DataAnnotations;
global using System.Globalization;
global using System.Net;
global using FluentValidation;
global using Graduation_Project.Features.Authentication.Commands.Models;
global using Graduation_Project.Services.Abstracts;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.IdentityModel.Tokens;
global using Graduation_Project.Services.Implemention;
global using Microsoft.IdentityModel.Tokens;
global using System.Security.Claims;
global using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UNITOOLDbContext>();
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("jwtSettings"));
builder.Services.AddAutoMapper(typeof(StartupBase));
builder.Services.AddServiceRegisteration(builder.Configuration);
builder.Services.AddServiceDependencies();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
