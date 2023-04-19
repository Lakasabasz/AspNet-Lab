using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.DTOs;
using Podstawy_widoków.MappingProfiles;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;
using Podstawy_widoków.Validators;
using Podstawy_widoków.ViewModels;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDatabaseInMemory>(options =>
    {
        options.UseInMemoryDatabase("Test").EnableSensitiveDataLogging();
    });
    builder.Services.AddScoped<IRepository<Reservation>, ReservationRepositoryInMemory>();
    builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepositoryInMemory>();
    builder.Services.AddScoped<IRepository<Localization>, LocalizationRepositoryInMemory>();
    builder.Services.AddScoped<IRepository<VehicleType>, VehicleTypesRepositoryInMemory>();
}

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDatabaseInMemory>();

builder.Services.AddAutoMapper(typeof(VehicleProfile));
builder.Services.AddScoped<IValidator<AddLocation>, AddLocationValidator>();
builder.Services.AddScoped<IValidator<AddReservation>, AddReservationValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();