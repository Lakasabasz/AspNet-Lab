using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.DTOs;
using Podstawy_widoków.MappingProfiles;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;
using Podstawy_widoków.Validators;
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

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDatabaseInMemory>();

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

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseInMemory>();
db.Database.EnsureCreated();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
var rootUser = new IdentityUser("root@localhost")
{
    EmailConfirmed = true
};
await userManager.CreateAsync(rootUser, "Root!2");
await userManager.AddToRoleAsync(rootUser, "admin");

var operatorUser = new IdentityUser("operator@localhost")
{
    EmailConfirmed = true
};
await userManager.CreateAsync(operatorUser, "Oper!1");
await userManager.AddToRoleAsync(operatorUser, "Operator");

var userUser = new IdentityUser("user@localhost")
{
    EmailConfirmed = true
};
await userManager.CreateAsync(userUser, "User!1");
await userManager.AddToRoleAsync(userUser, "User");

db.Reservations.Add(new Reservation()
{
    Claimer = userUser,
    Id = Guid.NewGuid(),
    ReservationDate = DateTime.Now.AddDays(-1),
    ReservationExpire = DateTime.Now.AddDays(5),
    Vehicle = db.Vehicles.First()
});
db.SaveChanges();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "UserManagement",
    areaName: "Admin",
    pattern: "Admin/{controller}/{action}/{id?}");

app.MapAreaControllerRoute(
    name: "ReservationList",
    areaName: "Users",
    pattern: "Users/{controller}/{action}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();