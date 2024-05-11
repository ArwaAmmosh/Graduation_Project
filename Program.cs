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
global using Graduation_Project.Services.Abstracts;
global using Graduation_Project.Services.Implemention;
global using Graduation_Project.Service;
global using Microsoft.AspNetCore.Mvc.Infrastructure;
global using Microsoft.AspNetCore.Mvc.Routing;
global using Graduation_Project.Entities.Identity;
global using Graduation_Project.Infrastructure.Abstract;
global using Graduation_Project.Infrastructure.Repository;
global using Microsoft.Extensions.Options;
global using FluentValidation;
global using Graduation_Project.Features.Authorization.Commands.Models;
global using Microsoft.Extensions.Localization;
global using SharpDX.DXGI;
global using IAuthorizationService = Graduation_Project.Services.Abstracts.IAuthorizationService;
global using Graduation_Project.Resource;
global using Graduation_Project.Seeder;
global using Graduation_Project.Helpers.DTOs;
global using Graduation_Project.Features.Authorization.Queries.Models;
global using Graduation_Project.Features.Authorization.Queries.Results;
global using MailKit.Net.Smtp;
global using MimeKit;
global using Graduation_Project.Features.Authentication.Command.Models;
global using Graduation_Project.Features.Authentication.Queries.Models;
global using System.IdentityModel.Tokens.Jwt;
global using Graduation_Project.Services.AuthServices.Interface;
global using Graduation_Project.Services.AuthServices.Implementation;
global using Graduation_Project.Dtos;
global using System.Security.Claims;
global using Graduation_Project.Filters;
global using Serilog;
global using Graduation_Project.MiddleWare;
global using System.Reflection;
global using Graduation_Project.Features.GuestMode.Command.Models;
global using Graduation_Project.Features.GuestMode.Queries.Results;
global using Graduation_Project.Features.GuestMode.Queries.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceRegisteration(builder.Configuration);
builder.Services.AddAuthentication();
builder.Services.AddDbContext<UNITOOLDbContext>();
builder.Services.AddScoped<CurrentUserService>(); // Added line
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
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});
builder.Services.AddTransient<AuthFilter>();
builder.Services.AddTransient<ToolManager>();
Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Services.AddSerilog();
var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await RoleSeeder.SeedAsync(roleManager);
   await UserSeeder.SeedAsync(userManager);
}

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
