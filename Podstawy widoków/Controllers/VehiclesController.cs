using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;
using Podstawy_widoków.ViewModels;

namespace Podstawy_widoków.Controllers;

public class VehiclesController : Controller
{
    private readonly IRepository<Vehicle> _vehicles;

    public VehiclesController(IRepository<Vehicle> vehicles)
    {
        _vehicles = vehicles;
    }

    public IActionResult List()
    {
        return View(new VehicleItemViewModel(
            _vehicles.GetAll()
            .Include(x => x.CurrentLocalization)
            .Include(x => x.Reservations)
            .Select(x => new VehicleRecordViewModel(x)).ToList()));
    }

    public IActionResult Details(Guid id)
    {
        if (id == Guid.Empty)
        {
            Response.StatusCode = 400;
            Console.WriteLine(_vehicles.GetAll().First().Id);
            return new EmptyResult();
        }
        var vm = _vehicles.GetAll()
            .Where(x=>x.Id == id)
            .Include(x => x.Reservations)
            .Include(x => x.CurrentLocalization)
            .Select(x => new VehicleDetailsViewModel(x))
            .First();
        return View(vm);
    }
}