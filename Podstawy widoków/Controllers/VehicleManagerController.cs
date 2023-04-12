using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.DTOs;
using Podstawy_widoków.Models;
using Podstawy_widoków.Services;
using Podstawy_widoków.ViewModels;

namespace Podstawy_widoków.Controllers;

public class VehicleManagerController : Controller
{
    private readonly IRepository<Vehicle> _vehicles;
    private readonly IRepository<VehicleType> _types;
    private readonly IRepository<Localization> _localizations;
    private readonly IMapper _mapper;

    public VehicleManagerController(IRepository<Vehicle> vehicles, IRepository<VehicleType> types, IRepository<Localization> localizations, IMapper mapper)
    {
        _vehicles = vehicles;
        _types = types;
        _localizations = localizations;
        _mapper = mapper;
    }

    public IActionResult Index(bool? status = null, string description = "")
    {
        return View(new VehicleManagerIndexViewModel(status is null ? null : (status.Value, description),
            _vehicles.GetAll()
                .Include(x => x.CurrentLocalization)
                .Include(x => x.Reservations)
                .Select(x => new VehicleRecordViewModel(x)).ToList()));
    }

    public IActionResult Add()
    {
        return View(new VehicleManagerAddViewModel(
            _types.GetAll().Select(x => x.Name).ToList(),
            _localizations.GetAll().Select(x => new LocalizationShortViewModel(x))));
    }

    public IActionResult AddItem(AddVehicle vehicle)
    {
        _vehicles.Add(_mapper.Map<Vehicle>(vehicle));
        _vehicles.SaveChanges();
        return RedirectToAction(nameof(Index), new { Status = true, Description = "Pomyślnie dodano" });
    }

    public IActionResult Details(Guid id)
    {
        var vehicle = _vehicles.Get(id);
        if(vehicle is null) return RedirectToAction(nameof(Index), new { Status = false, Description = "Nieznany indeks" });
        return View(_mapper.Map<VehicleDetailsViewModel>(vehicle));
    }

    public IActionResult EditItem(EditVehicle vehicle)
    {
        var self = _vehicles.Get(vehicle.Id);
        if(self is null) return RedirectToAction(nameof(Details), vehicle.Id);
        self.Description = vehicle.Description;
        self.Name = vehicle.Name;
        self.TypeId = vehicle.Type;
        self.CurrentLocalizationId = vehicle.Localization;
        self.ImageUrl = vehicle.ImageUrl;
        _vehicles.SaveChanges();
        return RedirectToAction(nameof(Details), vehicle.Id);
    }

    public IActionResult Edit(Guid id)
    {
        var vehicle = _vehicles.Get(id);
        if(vehicle is null) return RedirectToAction(nameof(Details), new { Id = id });
        return View(new VehicleManagerEditViewModel(
            _types.GetAll().Select(x => x.Name).ToList(),
            _localizations.GetAll().Select(x => new LocalizationShortViewModel(x)),
            vehicle));
    }

    public IActionResult Delete(Guid id)
    {
        _localizations.Remove(_localizations.Get(id));
        _localizations.SaveChanges();
        return RedirectToAction(nameof(Index), new { Status = true, Description = "Usunięto pomyślnie" });
    }
}