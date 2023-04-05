using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();